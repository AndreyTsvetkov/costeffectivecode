﻿using System;
using CostEffectiveCode.Ddd.Entities;

namespace CostEffectiveCode.Ddd.Pagination
{
    public abstract class Paging<TEntity, TOrderKey> : IPaging<TEntity, TOrderKey>
        where TEntity : class, IHasId
    {
        private readonly Sorting<TEntity, TOrderKey> _orderBy;

        private int _page;

        private int _take;

        protected Paging(int page, int take, Sorting<TEntity, TOrderKey> orderBy)
        {
            Page = page;
            Take = take;
            if (orderBy == null)
            {
                throw new ArgumentException("OrderBy can't be null", nameof(orderBy));
            }

            _orderBy = orderBy;
        }

        protected Paging()
        {
            Page = 1;
            Take = 30;
            // ReSharper disable once VirtualMemberCallInConstructor
            _orderBy = BuildDefaultSorting();
            if (_orderBy == null)
            {
                throw new ArgumentException("OrderBy can't be null", nameof(_orderBy));
            }
        }

        protected abstract Sorting<TEntity, TOrderKey> BuildDefaultSorting();

        public int Page
        {
            get { return _page; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("page must be >= 0", nameof(value));
                }

                _page = value;
            }
        }

        public int Take
        {
            get { return _take; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("take must be > 0", nameof(value));
                }

                _take = value;
            }
        }

        public Sorting<TEntity, TOrderKey> OrderBy => _orderBy;
    }
}
