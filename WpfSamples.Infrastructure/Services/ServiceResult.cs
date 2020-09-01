using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace WpfSamples.Infrastructure.Services
{
    public enum ServiceResult
    {
        [Display(Name = "成功")]
        SUCCESS = 0,
        [Display(Name = "エラー")]
        ERROR = 1,
        [Display(Name = "システムエラー")]
        SYSTEMERROR = 2,
        [Display(Name = "警告")]
        WARNING = 3,
        [Display(Name = "情報")]
        INFORMATION = 4,
        [Display(Name = "ユーザーエラー")]
        USERERROR = 5,
        [Display(Name = "注意")]
        ALERT = 6,
    }
}
