   M S B A M L   `   `   `  ����J  FWpfApplication1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:TestApp.ConvertersTestApp.Converters  5   	   7 clr-namespace:TestApp.ConvertersTestApp.Converters  5   	   !    TestApp.View.MainWindowView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   8databinding clr-namespace:TestApp.Converters     5   	   9databinding2 clr-namespace:TestApp.Converters     5   	     � Title$
  Test��5   	   $	��489��6   $	��504��6#    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5	   	    � WindowStyle$ 
ToolWindow=�5
   	    � 	Resources 5         GrayBlueGradientBrush �� 
StartPoint �� EndPoint � Color � Offset  BlueGradientBrush	 �� 
TargetType
 �� Property �� Value.   (TestApp.Converters.BooleanBrushConverter  BooleanBrushConverter$   TestApp.Converters.MyConverter  MyConverter��%  &	        &	 �     '�� �    7   '�     7  &	 �    &	 �    �� 5   
   $	 0,0&�6<   $	 1,1&�6M   .��5      � $ DarkGray��6   $ 0Q�6,   � 5      $ #CCCCFF��6   $	 0.5Q�6+   � 5      $ DarkGray��6   $ 1Q�6,   5      �� 5   
   $	 0,0&�68   $	 1,1&�6I   .��5      � $ Black��6   $ 0Q�6)   � 5      $ #CCCCFF��6   $	 0.5Q�6+   � 5      $ Black��6   $ 1Q�6)   5      �� 5   
   "	 ��6   .�5      �� 
 � ��8   6+   �� 5      
 � ��$ 80��6&   �� 5      
 � ��$ 10q�6'   5      �� 5   
   "	 �6   .�5      �� 
 � ��8   6+   �� 5       
 � ��$ 80��6&   �� 5!      
 � ��$ 10q�6'   5"        5#   
   6L     5$   
   69   5%      .��5'      � $��0q�6   $
��Left=�6   $	��504��61   .+�5(   
   �� -   5        $��questionBox��5(      $
��Left=�6%   $	��204��6@   $	��Top=�5)      $	��338��6*   $��	10,15,0,0q�66   ��5*      ��  �� Path$
 Test��� 5+   
   -   5        $	��tb1��5/      $
��Left=�5+      $��24��5,      $��367,184,0,0q�6    �� TextWrapping$
 Wrap=�5-      $�� ��6&   $	��Top=�5.      $��93��6*   5/      �� 51   
   -   5        $��test_button��51      $
��Test��6%   $
��Left=�64   $��
10,391,0,0q�6O   $	��Top=�52      $	��336��6)   $��34��66    J� RenderTransformOrigin$ 0.511,0.467&�6B   ��53      �� *TestCommand+�� 54   
   -   5        $��
add_button��54      $	��Add��6$   $
��Left=�62   $��33��6M   $��367,27,-104,0q�6Y   $	��Top=�6p   $��93��6�   ��55      �� *
AddCommand+��56      �� *Test+�� 57   
   -   5        $��remove_button��57      $��Remove��6'   $
��Left=�68   $��33��6S   $��367,75,-104,0q�6_   $	��Top=�6v   $��93��6�   ��58      �� *RemoveCommand+��63   ��  �� ElementName$ questionBox��$ SelectedItem��� 59   
   -   5        $��view_button��59      $
��View��6%   $
��Left=�64   $��33��6O   $��367,128,-104,0q�6[   $	��Top=�6s   $��93��6�   ��5:      �� *ViewCommand+��61   �� $ questionBox��$ SelectedItem��� 5;   
   -   5        $��Queury_button��5;      $��Queury��6'   $
��Left=�68   $��33��6S   $��367,271,0,0q�6_   $	��Top=�6t   $��93��6�   ��5<      �� *QueuryCommand+��5?   
   `� 5@       `� 	Converter# [ 6   .1�5A      �� $ SelectedIndex�6   $ 	Combobox1��6/   �� 5B      $
 Text�6   $	 tb1��6&   5C      5E      �� 5H   
   -   5        $��	answerBox��5H      $��
10,229,0,0q�6#   $
��Left=�5I      $	��Top=�6-   $	��338��6E   $	��112��5J   T   ��6   �� $ questionBox��$ SelectedItem�6T   �� 5L   
   -	   5        $��correct_label��5L      $��10,346,146,62q�5M      $��18(�5N      $��	ExtraBold"�5O      ��5P      �� $ 	answerBox��$ SelectedItem.Correct� �� 	Converter# [ ��5Q      �� $ 	answerBox��$ SelectedItem.Correct��� 5U   
   -
   5        $��	Combobox1��5V      $
��Left=�5U      $��367,229,0,0q�6.   $	��Top=�6C   $��93��6[    �� SelectedItem 5V   $   �� *SelectedItemProperty+ �� Mode$ TwoWay=�.@�5W      �� .��6   Sort5Y      �� 5Z      
���  ��.��6,   	FailStats5\      �� 5]      
��� � �.��6.   SuccessStats5_      �� 5`      
���**��.��6.   Group5b      �� 5c      
����  �.��6-   	Serialize5e      �� 5f      
��� ���.��6/   Deserialize5h      5i      5j      5k      