using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AK_Timetable.Components
{
    public partial class AKCommand : ComponentBase
    {
        [Parameter]
        public EventCallback OnClick { get; set; }

        [Parameter]
        public string Icon { get; set; }

        [Parameter]
        public bool IsDisabled { get; set; }

        private void OnButtonClick()
        {
            if (!IsDisabled)
                OnClick.InvokeAsync();
        }
    }
}
