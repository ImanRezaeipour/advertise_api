using System.Collections.Generic;
using System.Threading.Tasks;
using Advertise.Core.Common;

namespace Advertise.Service.Common
{
    public class ListService : IListService
    {
        public async Task<IList<SelectList>> GetIsBanListAsync()
        {
            var sortDirectionList = new List<SelectList>
            {
                new SelectList
                {
                    Value = string.Empty,
                    Text = "همه"
                },
                new SelectList
                {
                    Value = bool.TrueString,
                    Text = "قفل شده"
                },
                new SelectList
                {
                    Value = bool.FalseString,
                    Text = "قفل نشده"
                }
            };

            return sortDirectionList;
        }

        public async Task<IList<SelectList>> GetIsVerifyListAsync()
        {
            var sortDirectionList = new List<SelectList>
            {
                new SelectList
                {
                    Value = string.Empty,
                    Text = "همه"
                },
                new SelectList
                {
                    Value = bool.TrueString,
                    Text = "تائید شده"
                },
                new SelectList
                {
                    Value = bool.FalseString,
                    Text = "تائید نشده"
                }
            };

            return sortDirectionList;
        }

        public async Task<IList<SelectList>> GetPageSizeListAsync()
        {
            var pageSizeList = new List<SelectList>
            {
                new SelectList
                {
                    Value = PageSize.Count10.ToString(),
                    Text = "10"
                },
                new SelectList
                {
                    Value = PageSize.Count20.ToString(),
                    Text = "20"
                },
                new SelectList
                {
                    Value = PageSize.Count30.ToString(),
                    Text = "30"
                },
                new SelectList
                {
                    Value = PageSize.Count50.ToString(),
                    Text = "50"
                },
                new SelectList
                {
                    Value = PageSize.Count100.ToString(),
                    Text = "100"
                },
                new SelectList
                {
                    Value = "10000",
                    Text = "همه"
                }
            };

            return pageSizeList;
        }

        public async Task<IList<SelectList>> GetSortDirectionFilterListAsync()
        {
            var sortList = new List<SelectList>
            {
                new SelectList
                {
                    Value = "asc",
                    Text = "صعودی"
                },
                new SelectList
                {
                    Value = "desc",
                    Text = "نزولی"
                }
            };

            return sortList;
        }

        public async Task<IList<SelectList>> GetSortDirectionListAsync()
        {
            var sortDirectionList = new List<SelectList>
            {
                new SelectList
                {
                    Value = SortDirection.Asc,
                    Text = "صعودی"
                },
                new SelectList
                {
                    Value = SortDirection.Desc,
                    Text = "نزولی"
                }
            };

            return sortDirectionList;
        }

        public async Task<IList<SelectList>> GetSortMemberFilterListAsync()
        {
            var sortList = new List<SelectList>
            {
                new SelectList
                {
                    Value = "price",
                    Text = "قیمت"
                },
                new SelectList
                {
                    Value = "createdon",
                    Text = "جدیدترین"
                }
            };

            return sortList;
        }

        public async Task<IList<SelectList>> GetSortMemberListByTitleAsync()
        {
            var sortMemberList = new List<SelectList>
            {
                new SelectList
                {
                    Value = SortMember.CreatedOn,
                    Text = "تاریخ درج"
                },
                new SelectList
                {
                    Value = SortMember.ModifiedOn,
                    Text = "آخرین تغییر"
                },
                new SelectList
                {
                    Value = SortMember.Title,
                    Text = "نام"
                }
            };

            return sortMemberList;
        }
    }
}