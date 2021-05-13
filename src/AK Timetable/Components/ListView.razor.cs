using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AK_Timetable.Components
{
    public partial class ListView<T> : ComponentBase
    {

        [Parameter]
        public List<T> Items { get; set; }

        [Parameter]
        public T SelectedItem { get; set; }

        [Parameter]
        public EventCallback<T> SelectedItemChanged { get; set; }

        [Parameter]
        public RenderFragment<T> ItemTemplate { get; set; }

        private void ItemClick(T item)
        {
            SelectedItem = item;
            SelectedItemChanged.InvokeAsync(item);
        }

    }
}
