using BaseProject.Models;
using System.Text;

namespace WebApp.Template.UserCards
{
    public abstract class UserCardTemplate
    {
        protected AppUser _appUser { get; set; }

        public void SetUser(AppUser appUser)
        {
            _appUser = appUser;
        }
        protected abstract string SetFooter();
        protected abstract string SetPicture();
        public string Build()
        {
            if (_appUser == null) throw new ArgumentNullException(nameof(AppUser));

            var sb = new StringBuilder();

            sb.Append("<div class='card'>");
            sb.Append(SetPicture());
            sb.Append($@"<div class='card-body'>
                          <h5>{_appUser.UserName}</h5>
                          <p>{_appUser.Description}</p>");
            sb.Append(SetFooter());
            sb.Append("</div>");
            sb.Append("</div>");

            return sb.ToString();
        }
    }
}
