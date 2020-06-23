using System;
using System.ComponentModel.DataAnnotations;

namespace AssessmentApplication.Models.Account
{
    [Serializable]
    public class LoginModel
    {
        #region Public Properties

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required(ErrorMessage = "required")]
        public string Password { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "required")]
        public string Username { get; set; }

        #endregion Public Properties
    }
}