using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SOATester.Infrastructure.Collections {
    public class SortableObservableCollection<T> : ObservableCollection<T> {
        public SortableObservableCollection() : base() { }
        public SortableObservableCollection(List<T> l) : base(l) { }
        public SortableObservableCollection(IEnumerable<T> l) : base(l) { }

        #region IndexOf

        /// <summary>
        /// Returns the index of the first object which meets the specified function
        /// </summary>
        /// <param name="keySelector">A bool function to compare each Item by</param>
        /// <returns>The index of the first Item which matches the function</returns>
        public int IndexOf(Func<T, bool> compareFunction) {
            return Items.IndexOf(Items.FirstOrDefault(compareFunction));
        }

        #endregion

        #region Sorting

        /// <summary>
        /// Sorts the items of the collection in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <param name="keySelector">A function to extract a key from an item.</param>
        public void Sort<TKey>(Func<T, TKey> keySelector) {
            InternalSort(Items.OrderBy(keySelector));
        }

        /// <summary>
        /// Sorts the items of the collection in descending order according to a key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <param name="keySelector">A function to extract a key from an item.</param>
        public void SortDescending<TKey>(Func<T, TKey> keySelector) {
            InternalSort(Items.OrderByDescending(keySelector));
        }

        /// <summary>
        /// Sorts the items of the collection in ascending order according to a key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <param name="keySelector">A function to extract a key from an item.</param>
        /// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
        public void Sort<TKey>(Func<T, TKey> keySelector, IComparer<TKey> comparer) {
            InternalSort(Items.OrderBy(keySelector, comparer));
        }

        /// <summary>
        /// Moves the items of the collection so that their orders are the same as those of the items provided.
        /// </summary>
        /// <param name="sortedItems">An <see cref="IEnumerable{T}"/> to provide item orders.</param>
        private void InternalSort(IEnumerable<T> sortedItems) {
            var sortedItemsList = sortedItems.ToList();

            foreach (var item in sortedItemsList) {
                Move(IndexOf(item), sortedItemsList.IndexOf(item));
            }
        }

        #endregion
    }
}
