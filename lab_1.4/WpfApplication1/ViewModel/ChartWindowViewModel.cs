using System;
using System.Windows.Input;
using TestApp.Model;
using TestApp.View;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;
using System.Drawing;

namespace TestApp.ViewModel
{
    class ChartWindowViewModel
    {
        public ICommand ReturnCommand { get; set; }
        ChartWindowView chartWindow;

        public ChartWindowViewModel(ChartWindowView chartWindow, IEnumerable<Question> test, int mode)
        {
            this.chartWindow = chartWindow;
            DrawChart(test, mode);          
            ReturnCommand = new Commands.ReturnCommand(chartWindow);
        }

        public void DrawChart(IEnumerable<Question> test, int mode)
        {
            Canvas myCanvas = chartWindow.canvas1;

            TextBlock textBlock3 = new TextBlock();
            textBlock3.Text = "Index";
            Canvas.SetTop(textBlock3, 10);
            Canvas.SetLeft(textBlock3, 5);
            Canvas.SetRight(textBlock3, 15);
            myCanvas.Children.Add(textBlock3);

            TextBlock textBlock4 = new TextBlock();
            textBlock4.Text = "Value";
            Canvas.SetTop(textBlock4, myCanvas.Height - 30);
            Canvas.SetLeft(textBlock4, 5);
            Canvas.SetRight(textBlock4, 15);
            myCanvas.Children.Add(textBlock4);

            PointCollection myPointCollectionH = new PointCollection();
            int count = 0;
            foreach (var q in test)
            {
                TextBlock textBlock = new TextBlock();
               
                myPointCollectionH.Add(new Point(count, 0));
                if (mode == 0)
                {                                                   
                    myPointCollectionH.Add(new Point(count, -q.FailStats));
                    count++;
                    myPointCollectionH.Add(new Point(count, -q.FailStats));                  
                }
                else if (mode == 1)
                {
                    myPointCollectionH.Add(new Point(count, -q.SuccessStats));
                    count++;
                    myPointCollectionH.Add(new Point(count, -q.SuccessStats));
                }

                TextBlock textBlock2 = new TextBlock();
                textBlock2.Text = mode == 0 ? q.FailStats.ToString() : q.SuccessStats.ToString();
                Canvas.SetTop(textBlock2, myCanvas.Height - 30);
                Canvas.SetLeft(textBlock2, count * 50 + 20);
                Canvas.SetRight(textBlock2, count * 50 + 35);
                myCanvas.Children.Add(textBlock2);

                myPointCollectionH.Add(new Point(count, 0));
                textBlock.Text = q.Index.ToString();
                Canvas.SetTop(textBlock, 10);
                Canvas.SetLeft(textBlock, count * 50 + 20);
                Canvas.SetRight(textBlock, count * 50 + 35);
                myCanvas.Children.Add(textBlock);
            }

            Polygon myPolygonH = new Polygon();
            myPolygonH.Points = myPointCollectionH;
            if (mode == 0)
            {
                myPolygonH.Fill = Brushes.Red;
            }
            if (mode == 1)
            {
                myPolygonH.Fill = Brushes.Green;
            }         

            myPolygonH.Width = count * 50;
            myPolygonH.Height = myCanvas.Height - 70;
            myPolygonH.Stretch = Stretch.Fill;
            myPolygonH.Stroke = Brushes.Black;
            myPolygonH.StrokeThickness = 2;

           
            Canvas.SetLeft(myPolygonH, 50);
            Canvas.SetTop(myPolygonH, 30);
            Canvas.SetBottom(myPolygonH, 50);
            myCanvas.Children.Add(myPolygonH);
            
            if (count > 6)
            {
                myCanvas.Width = count * 50 + 100;
            }
        }
    }
}
