# How to conditionally handle the swiping in Xamarin.Forms ListView (SfListView)

You can enable or disable [ListViewItem](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.ListViewItem.html?) swiping conditionally in Xamarin.Forms [SfListView](https://help.syncfusion.com/xamarin/listview/overview?) using the binding context property.

You can also refer the following article.
https://www.syncfusion.com/kb/11669/how-to-conditionally-handle-the-swiping-in-xamarin-forms-listview-sflistview 

**XAML**

Defined **SfListView** with [LeftSwipeTemplate](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~LeftSwipeTemplate.html?), [RightSwipeTemplate](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~RightSwipeTemplate.html?) and set the [AllowSwiping](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~AllowSwiping.html?) Property to **True**
``` xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:local="clr-namespace:ListViewXamarin"
             xmlns:syncfusion="clr-namespace:Syncfusion.ListView.XForms;assembly=Syncfusion.SfListView.XForms"
             x:Class="ListViewXamarin.MainPage">
 
    <ContentPage.BindingContext>
        <local:ContactsViewModel/>
    </ContentPage.BindingContext>
 
    <ContentPage.Behaviors>
        <local:Behavior/>
    </ContentPage.Behaviors>
 
    <ContentPage.Content>
        <StackLayout>
            <syncfusion:SfListView x:Name="listView" ItemSpacing="1" AllowSwiping="True" AutoFitMode="Height" SelectionMode="None" ItemsSource="{Binding contactsinfo}">
                <syncfusion:SfListView.ItemTemplate >
                    <DataTemplate>
                        <ViewCell>
                            <ViewCell.View>
                                <Grid x:Name="grid" BackgroundColor="{Binding BackgroundColor}">
                                    <Grid.RowDefinitions>
                                        <RowDefinition Height="*" />
                                    </Grid.RowDefinitions>
                                    <Grid RowSpacing="0">
                                        <Grid.ColumnDefinitions>
                                            <ColumnDefinition Width="70" />
                                            <ColumnDefinition Width="*" />
                                        </Grid.ColumnDefinitions>
 
                                        <Image Source="{Binding ContactImage}" VerticalOptions="Center" HorizontalOptions="Center" HeightRequest="50" WidthRequest="50"/>
 
                                        <Grid Grid.Column="1" RowSpacing="1" VerticalOptions="Center">
                                            <Grid.RowDefinitions>
                                                <RowDefinition Height="*" />
                                                <RowDefinition Height="*" />
                                            </Grid.RowDefinitions>
                                            <Label Text="{Binding ContactName}"/>
                                            <Label Grid.Row="1" Grid.Column="0" Text="{Binding ContactNumber}"/>
                                        </Grid>
                                    </Grid>
                                </Grid>
                            </ViewCell.View>
                        </ViewCell>
                    </DataTemplate>
                </syncfusion:SfListView.ItemTemplate>
                <syncfusion:SfListView.LeftSwipeTemplate>
                    <DataTemplate>
                        <Grid BackgroundColor="Black">
                            <Label Text="Left Swipe" TextColor="White" FontAttributes="Bold" VerticalTextAlignment="Center" HorizontalTextAlignment="Center"/>
                        </Grid>
                    </DataTemplate>
                </syncfusion:SfListView.LeftSwipeTemplate>
                <syncfusion:SfListView.RightSwipeTemplate>
                    <DataTemplate>
                        <Grid BackgroundColor="Black">
                            <Label Text="Right Swipe" TextColor="White" FontAttributes="Bold" VerticalTextAlignment="Center" HorizontalTextAlignment="Center"/>
                        </Grid>
                    </DataTemplate>
                </syncfusion:SfListView.RightSwipeTemplate>
            </syncfusion:SfListView>
        </StackLayout>
    </ContentPage.Content>
</ContentPage>
```
**C#**

Defining **BackgroundColor** property in **Model**
``` c#
namespace ListViewXamarin
{
    public class Contacts : INotifyPropertyChanged
    {
        …
        private Color backgroundColor;
 
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                backgroundColor = value;
                this.RaisedOnPropertyChanged("BackgroundColor");
            }
        }
 
        public Contacts()
        {
 
        }
 
        public event PropertyChangedEventHandler PropertyChanged;
 
        public void RaisedOnPropertyChanged(string _PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(_PropertyName));
            }
        }
    }
}
```
**C#**

Populating **BackgroundColor** property in the **ViewModel**
``` c#
namespace ListViewXamarin
{
    public class ContactsViewModel
    {
        public ObservableCollection<Contacts> contactsinfo { get; set; }
 
        public ContactsViewModel()
        {
            contactsinfo = new ObservableCollection<Contacts>();
            GenerateInfo();
        }
 
        public void GenerateInfo()
        {
            …
            for (int i = 0; i < CustomerNames.Count(); i++)
            {
                …
                if(i%2 == 0)
                {
                    contact.BackgroundColor = Color.LightGreen;
                }
                else
                {
                    contact.BackgroundColor = Color.LightGray;
                }
                contactsinfo.Add(contact);
            }
        }
    }
}
```
**C#**

Based on the **BackgroundColor** property the swipe is disabled

``` c#
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
```
**Output**

![ConditionalSwiping](https://github.com/SyncfusionExamples/conditional-swiping-listview-xamarin/blob/master/ScreenShots/ConditionalSwiping.gif)
