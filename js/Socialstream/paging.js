
define([], function () {
    var self = this;

    var paging = {

        pageIndex: {
            pageIndex: 0, get: function () {
                if (!this.Initialized)
                { this.Initialized = true; }
                return this.pageIndex;
            },
            set: function (index) { this.pageIndex = index; },
            Initialized: false
        },
        pageSize: { pageSize: 2, get: function () { return this.pageSize; }, set: function (pagesize) { this.pageSize = pagesize; } },
        currentIndex: { get: function () { return paging.pageIndex.get(); }, set: function (index) { paging.pageIndex.set(index); } },
        nextIndex: {
            get: function () {
                if (!paging.pageIndex.Initialized) {
                    return paging.pageIndex.get();
                } else { paging.pageIndex.set(paging.currentIndex.get() + 1); return paging.pageIndex.get(); }

            }
        }
    };

    return paging;
});
