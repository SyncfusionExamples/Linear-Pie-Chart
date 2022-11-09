# How to visualize the Xamarin.Forms Pie Chart in linear form (SfChart)?

This article explains the way to render the pie chart in linear form. By extending the Xamarin Chart with the [StackingColumnSeries](https://help.syncfusion.com/xamarin/charts/charttypes#stacked-column-chart) and hiding the primary and secondary axes, we can show the pie chart in linear form as follows.

```
<local:PieChartExt x:Name="Chart" ItemsSource="{Binding Data}" XBindingPath="Component" YBindingPath="Price" HorizontalOptions="FillAndExpand" VerticalOptions="FillAndExpand"> 
  
            <chart:SfChart.Title> 
                <chart:ChartTitle Text="A Linear PieChart"  /> 
            </chart:SfChart.Title> 
            <chart:SfChart.BindingContext> 
                <local:ViewModel x:Name="viewModel" /> 
            </chart:SfChart.BindingContext> 
            <chart:SfChart.PrimaryAxis> 
                <chart:CategoryAxis IsVisible="False" ShowMajorGridLines="False" > 
                </chart:CategoryAxis> 
            </chart:SfChart.PrimaryAxis> 
                <chart:SfChart.SecondaryAxis> 
                    <chart:NumericalAxis IsVisible="False" ShowMajorGridLines="False"> 
                    </chart:NumericalAxis> 
                </chart:SfChart.SecondaryAxis> 
            <chart:SfChart.ColorModel> 
                <chart:ChartColorModel Palette="Natural"/> 
            </chart:SfChart.ColorModel>
</local:PieChartExt>
```

```
public class PieChartExt : SfChart 
{
        ….
        public string XBindingPath 
        { 
            get { return (string)GetValue(XBindingPathProperty); } 
            set { SetValue(XBindingPathProperty, value); } 
        } 
  
        public string YBindingPath 
        { 
            get { return (string)GetValue(YBindingPathProperty); } 
            set { SetValue(YBindingPathProperty, value); } 
        } 
  
        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue) 
        { 
            (bindable as PieChartExt).GenerateSeries(newValue); 
        } 
  
        private void GenerateSeries(object newValue) 
        { 
            ..
  
                while (commonItemsSource.MoveNext()) 
                { 
                    CreateSeries(commonItemsSource.Current); 
                } 
            ..
        }  
 
      private void CreateSeries(object newValue)
      {
            StackingBar100Series stackingBar100Series = new StackingBar100Series()
            {
                ItemsSource = new List<PriceData> { newValue as PriceData },
                XBindingPath = XBindingPath,
                YBindingPath = YBindingPath,
                Width = 0.7,
            };
            
            stackingBar100Series.DataMarker = new ChartDataMarker();
            stackingBar100Series.DataMarker.ShowLabel = true;
            stackingBar100Series.DataMarker.LabelStyle.TextColor = Color.White;
            stackingBar100Series.DataMarker.LabelStyle.LabelPosition = DataMarkerLabelPosition.Center;
            stackingBar100Series.DataMarkerPosition = DataMarkerPosition.Center;
            Series.Add(stackingBar100Series);       
      }
… 
}
```

## Output:

![Linear Pie Chart Xamarin.Forms](https://user-images.githubusercontent.com/53489303/200723209-9be7bd26-a4d9-43b0-b849-cd520334e294.png)

KB article - [How to visualize the Xamarin.Forms Pie Chart in linear form?](https://www.syncfusion.com/kb/11285/how-to-visualize-the-xamarin-forms-pie-chart-in-linear-form)
