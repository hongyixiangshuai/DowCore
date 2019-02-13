using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cassius.App.Users.Dto
{
    public class PagedUserResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
        public bool? IsActive { get; set; }
    }
}
