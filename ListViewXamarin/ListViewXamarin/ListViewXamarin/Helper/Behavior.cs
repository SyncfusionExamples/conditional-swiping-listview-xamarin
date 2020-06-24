using Syncfusion.DataSource.Extensions;
using Syncfusion.GridCommon.ScrollAxis;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewXamarin
{
    public class Behavior : Behavior<ContentPage>
    {
        SfListView sfListView;
        protected override void OnAttachedTo(ContentPage bindable)
        {
            sfListView = bindable.FindByName<SfListView>("listView");
            sfListView.SwipeStarted += SfListView_SwipeStarted;
            base.OnAttachedTo(bindable);
        }

        private void SfListView_SwipeStarted(object sender, Syncfusion.ListView.XForms.SwipeStartedEventArgs e)
        {
            if((e.ItemData as Contacts).BackgroundColor == Color.LightGray)
            {
                e.Cancel = true;
            }
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            sfListView.SwipeStarted -= SfListView_SwipeStarted;
            sfListView = null;
            base.OnDetachingFrom(bindable);
        }
    }
}