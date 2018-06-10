using Finegamedesign.Utils;
using System;
using System.Collections.Generic;

namespace Finegamedesign.Entitas
{
    public static class ReceiverUtils
    {
        private const int kEmpty = -1;

        public static bool IsEmpty(ReceiverComponent receiver)
        {
            return receiver.occupantId == kEmpty;
        }

        public static bool AllFull(GameContext context, int[] receiverIds)
        {
            foreach (int receiverId in receiverIds)
            {
                GameEntity receiver = context.GetEntityWithId(receiverId);
                if (ReceiverUtils.IsEmpty(receiver.receiver))
                    return false;
            }
            return true;
        }

        /// <returns>
        /// If receiver is empty and accepts a component of the given name.
        /// </returns>
        public static bool Filter(ReceiverComponent receiver, string componentName)
        {
            if (!IsEmpty(receiver))
                return false;

            HashSet<int> filterIndexes = receiver.filterComponentIndexes;
            if (filterIndexes == null)
                return false;

            string[] componentNames = GameComponentsLookup.componentNames;
            int componentIndex = Array.IndexOf(componentNames, componentName);
            DebugUtil.Assert(componentIndex >= 0,
                "ReceiverUtils.Filter: Expects component '" + componentName + "' in game component names " +
                DataUtil.ToString(componentNames));

            return filterIndexes.Contains(componentIndex);
        }

        /// <returns>
        /// TODO: If candidate has one of the filter components.
        /// </returns>
        #warning TODO: If candidate has one of the filter components.
        public static bool Filter(ReceiverComponent receiver, GameEntity candidate)
        {
            DebugUtil.Assert(false, "ReceiverUtils.Filter: TODO: If candidate has one of the filter components.");
            return false;
        }

        public static void ReplaceOccupant(GameEntity receiver, GameEntity occupant)
        {
            receiver.ReplaceReceiver(
                receiver.receiver.filterComponentIndexes,
                occupant.id.value
            );
        }

        public static void SetEmpty(GameEntity receiver)
        {
            receiver.ReplaceReceiver(
                receiver.receiver.filterComponentIndexes,
                kEmpty
            );
        }

        public static void SetEmpty(GameContext context, int[] receiverIds)
        {
            foreach (int receiverId in receiverIds)
            {
                GameEntity receiver = context.GetEntityWithId(receiverId);
                SetEmpty(receiver);
            }
        }

        /// <param name="preferredIds">
        /// Selects one of these first if acceptable.  Sets that preferred ID to empty if selected.
        /// </param>
        public static void OccupyIfEmpty(GameContext context, int[] receiverIds, int[] preferredIds)
        {
            foreach (int receiverId in receiverIds)
            {
                GameEntity receiver = context.GetEntityWithId(receiverId);
                if (!IsEmpty(receiver.receiver))
                    continue;

                GameEntity occupant = null;
                for (int preferredIndex = 0, numPreferred = preferredIds.Length; preferredIndex < numPreferred; ++ preferredIndex)
                {
                    int preferredId = preferredIds[preferredIndex];
                    GameEntity preferred = context.GetEntityWithId(preferredId);
                    if (preferred == null)
                        continue;

                    if (!Filter(receiver.receiver, preferred))
                        continue;

                    occupant = preferred;
                }

                if (occupant == null)
                {
                    occupant = context.CreateEntity();
                    #warning TODO: Add component of first name in receiver filter.
                    DebugUtil.Assert(false, "ReceiverUtils.OccupyIfEmpty: TODO: Add component of first name in receiver filter.");
                }

                ReplaceOccupant(receiver, occupant);
            }
        }

        /// <summary>
        /// TODO
        /// Receiver might be an input or an output of a transmitter.
        /// As in a Petri Net, only transmits if all receivers are occupied.
        /// Unlike a Petri Net, only transmits if to empty outputs.
        /// Also unlike a Petri Net, preserves the input entity if it would be a suitable output.
        ///
        /// To deposit into one of many alternatives, output to a receiver selector.
        ///
        /// Expects each input and output has a receiver, or else Entitas throws an exception.
        /// </summary>
        public static void TryTransmit(GameContext context, GameEntity receiver, int[] inputIds, int[] outputIds)
        {
            if (!AllFull(context, inputIds))
                return;

            OccupyIfEmpty(context, outputIds, inputIds);

            SetEmpty(context, inputIds);
        }
    }
}
