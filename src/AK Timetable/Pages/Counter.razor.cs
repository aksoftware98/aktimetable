using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK_Timetable.Pages
{
    [Authorize]
    public partial class Counter : ComponentBase
    {
        
        [Inject]
        public ITokenAcquisition TokenService { get; set; }

        [Inject]
        public GraphServiceClient Graph { get; set; }

        [Inject]
        public MicrosoftIdentityConsentAndConditionalAccessHandler ConsentHandler { get; set; }

        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        private string _name = string.Empty; 

        private async Task DoSomethingAsync()
        {
            try
            {
                var token = await TokenService.GetAuthenticationResultForUserAsync(new[] { "User.Read", "Calendars.ReadWrite" });
                Console.WriteLine(token.AccessToken);

                await Graph.Me.Calendar.Events.Request().AddAsync(new Event
                {
                    Subject = "Hey stupid",
                    Start = DateTimeTimeZone.FromDateTime(DateTime.Now),
                    End = DateTimeTimeZone.FromDateTime(DateTime.Now.AddMinutes(15))
                });
                _name = (await Graph.Me.Request().GetAsync()).DisplayName; 
            }
            catch (Exception ex)
            {
                ConsentHandler.HandleException(ex);
            }
            

            
        }
    }
}
