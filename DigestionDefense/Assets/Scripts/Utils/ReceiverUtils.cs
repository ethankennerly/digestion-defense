// Uncomment to log some function calls.
// #define LOG_RECEIVER

using FineGameDesign.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FineGameDesign.Entitas
{
    public static class ReceiverUtils
    {
        public const int kEmpty = -1;

        public static bool IsEmpty(ReceiverComponent receiver)
        {
            foreach (int occupantId in receiver.occupantIds)
                if (occupantId != kEmpty)
                    return false;

            return true;
        }

        // TODO: Gets quantity of each occupant.
        public static bool IsFull(ReceiverComponent receiver)
        {
            return receiver.availableIndex < 0;
        }

        public static bool AnyEmpty(GameContext context, int[] receiverIds)
        {
            Log("AnyEmpty");

            foreach (int receiverId in receiverIds)
            {
                GameEntity receiver = context.GetEntityWithId(receiverId);
                if (IsEmpty(receiver.receiver))
                    return true;
            }

            return false;
        }

        /// <returns>
        /// If receiver is empty and accepts a component of the given name.
        /// </returns>
        public static bool Filter(ReceiverComponent receiver, int componentIndex)
        {
            DebugUtil.Assert(componentIndex >= 0 && componentIndex < GameComponentsLookup.componentNames.Length,
                "ReceiverUtils.Filter: Expects component index " + componentIndex +
                " is in range of game component names " +
                DataUtil.ToString(GameComponentsLookup.componentNames));

            if (IsFull(receiver))
                return false;

            HashSet<int> filterIndexes = receiver.filterComponentIndexes;
            if (filterIndexes == null)
            {
                DebugUtil.Assert(filterIndexes != null,
                    "ReceiverUtils.Filter: Expects filter component indexes are defined on " + receiver +
                    ". componentIndex='" + componentIndex + "'");
                return false;
            }

            return filterIndexes.Contains(componentIndex);
        }

        /// <returns>
        /// If receiver is empty and accepts a component of the given name.
        /// </returns>
        public static bool Filter(ReceiverComponent receiver, string componentName)
        {
            if (IsFull(receiver))
                return false;

            HashSet<int> filterIndexes = receiver.filterComponentIndexes;
            if (filterIndexes == null)
            {
                DebugUtil.Assert(filterIndexes != null,
                    "ReceiverUtils.Filter: Expects filter component indexes are defined on " + receiver +
                    ". componentName='" + componentName + "'");
                return false;
            }

            int componentIndex = ToComponentIndex(componentName);

            return filterIndexes.Contains(componentIndex);
        }

        /// <remarks>
        /// Iterating would scale better if filter names were:
        ///     (A) a bitfield instead of a hash set.  Or,
        ///     (B) an array instead of a hash set.
        /// </remarks>
        /// <returns>
        /// If receiver is empty and candidate has any of the receiver's filter components.
        /// </returns>
        public static bool Filter(ReceiverComponent receiver, GameEntity candidate)
        {
            if (IsFull(receiver))
                return false;

            HashSet<int> filterIndexes = receiver.filterComponentIndexes;
            if (filterIndexes == null)
            {
                DebugUtil.Assert(filterIndexes != null,
                    "ReceiverUtils.Filter: Expects filter component indexes are defined on " + receiver);
                return false;
            }

            foreach (int filterIndex in filterIndexes)
            {
                if (candidate.HasComponent(filterIndex))
                    return true;
            }

            return false;
        }

        public static int ToComponentIndex(string componentName)
        {
            string[] componentNames = GameComponentsLookup.componentNames;
            int componentIndex = Array.IndexOf(componentNames, componentName);
            DebugUtil.Assert(componentIndex >= 0,
                "ReceiverUtils.Filter: Expects component '" + componentName + "' in game component names " +
                DataUtil.ToString(componentNames));
            return componentIndex;
        }

        public static void AddOccupant(GameEntity receiver, int occupantId)
        {
            Log("AddOccupant");

            ReceiverComponent component = receiver.receiver;
            component.occupantIds[component.availableIndex] = occupantId;
            component.availableIndex = GetNextAvailableIndex(component);
            receiver.ReplaceComponent(GameComponentsLookup.Receiver, component);
        }

        public static int GetNextAvailableIndex(ReceiverComponent component)
        {
            int availableIndex = component.availableIndex;
            int maxOccupants = component.occupantIds.Length;
            for (int offset = 0; offset < maxOccupants; ++offset)
            {
                int occupantIndex = (offset + availableIndex) % maxOccupants;
                if (component.occupantIds[occupantIndex] == kEmpty)
                    return occupantIndex;
            }
            return -1;
        }

        private static int GetNextOccupantId(ReceiverComponent component)
        {
            foreach (int occupantId in component.occupantIds)
                if (occupantId != kEmpty)
                    return occupantId;

            return -1;
        }

        public static void RemoveOccupant(GameEntity receiver, int occupantId)
        {
            ReceiverComponent component = receiver.receiver;
            int[] occupantIds = component.occupantIds;
            int occupantIndex = Array.IndexOf(occupantIds, occupantId);
            DebugUtil.Assert(occupantIndex != -1,
                "RemoveOccupant: Expected occupant ID=" + occupantId +
                " found in occupant IDs=" + DataUtil.ToString(occupantIds) +
                ". So an array out of bounds error will occur."
            );
            component.occupantIds[occupantIndex] = kEmpty;
            component.availableIndex = GetNextAvailableIndex(component);
            receiver.ReplaceComponent(GameComponentsLookup.Receiver, component);
        }

        public static void SetEmpty(GameEntity receiver)
        {
            ReceiverComponent component = receiver.receiver;
            for (int occupantIndex = component.occupantIds.Length - 1; occupantIndex >= 0; --occupantIndex)
                component.occupantIds[occupantIndex] = kEmpty;

            receiver.ReplaceComponent(GameComponentsLookup.Receiver, component);
        }

        public static void SetEmpty(GameContext context, int[] receiverIds)
        {
            foreach (int receiverId in receiverIds)
            {
                GameEntity receiver = context.GetEntityWithId(receiverId);
                SetEmpty(receiver);
            }
        }

        /// <summary>
        /// Receiver might be an input or an output of a transmitter.
        /// As in a Petri Net, only transmits if all receivers are occupied.
        /// Unlike a Petri Net, only transmits if to outputs with an empty occupant position.
        /// Also unlike a Petri Net, preserves the input entity if it would be a suitable output.
        ///
        /// Expects each input and output has a receiver, or else Entitas throws an exception.
        /// </summary>
        ///
        /// <param name="giverIds">
        /// Selects first acceptable giver or gift in that giver's receiver.  Sets that giver ID to empty if selected.
        /// </param>
        public static void TryOccupy(GameContext context, int[] receiverIds, int[] giverIds)
        {
            Log("TryOccupy");

            GameEntity[] giverEntities = GameLinkUtils.GetEntitiesWithIds(context, giverIds);
            foreach (GameEntity giverEntity in giverEntities)
            {
                int giftId = GetNextOccupantId(giverEntity.receiver);
                if (giftId == kEmpty)
                    continue;

                RemoveOccupant(giverEntity, giftId);

                GameEntity gift = context.GetEntityWithId(giftId);
                DebugUtil.Assert(gift != null,
                    "ReceiverUtils.TryOccupy: Expected gift was not null. Gift ID=" + giftId);
                if (gift == null)
                    continue;

                foreach (int receiverId in receiverIds)
                {
                    GameEntity receiver = context.GetEntityWithId(receiverId);
                    ReceiverComponent component = receiver.receiver;
                    if (IsFull(component))
                        continue;

                    if (Filter(component, giverEntity))
                    {
                        AddOccupant(receiver, giverEntity.id.value);
                        break;
                    }

                    if (Filter(component, gift))
                    {
                        AddOccupant(receiver, giftId);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// As in a Petri Net, only transmits if all receivers are occupied.
        /// Unlike a Petri Net, arcs do not (yet) support quantity other than one.
        /// </summary>
        public static void TryTransmit(GameContext context,
            int[] inputIds, int[] outputIds)
        {
            if (AnyEmpty(context, inputIds))
                return;

            if (TryGiveQuantity(context, outputIds, inputIds))
                return;

            TryOccupy(context, outputIds, inputIds);
        }

        /// <summary>
        /// Gives quantity from giver to receiver without exchanging the entities.
        /// </summary>
        public static bool TryGiveQuantity(GameContext context, int[] receiverIds, int[] giverIds)
        {
            Log("TryGiveQuantity");

            bool gaveAnything = false;
            GameEntity[] giverEntities = GameLinkUtils.GetEntitiesWithIds(context, giverIds);
            foreach (GameEntity giverEntity in giverEntities)
            {
                int giftId = GetNextOccupantId(giverEntity.receiver);
                if (giftId == kEmpty)
                    continue;

                GameEntity gift = context.GetEntityWithId(giftId);
                DebugUtil.Assert(gift != null,
                    "ReceiverUtils.TryOccupy: Expected gift was not null. Gift ID=" + giftId);
                if (gift == null)
                    continue;

                if (!gift.hasQuantity || gift.quantity.value == 0)
                    continue;

                foreach (int receiverId in receiverIds)
                {
                    GameEntity receiver = context.GetEntityWithId(receiverId);
                    ReceiverComponent component = receiver.receiver;
                    if (!Filter(component, gift))
                        continue;

                    if (!receiver.hasQuantity)
                        continue;

                    receiver.ReplaceQuantity(receiver.quantity.value + gift.quantity.value);
                    gift.ReplaceQuantity(0);
                    gaveAnything = true;
                    break;
                }
            }
            return gaveAnything;
        }

        [Conditional("LOG_RECEIVER")]
        private static void Log(string message)
        {
            DebugUtil.Log(message);
        }
    }
}
