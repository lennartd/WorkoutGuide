   M S B A M L   `   `   `  ����G  CWorkoutGuide, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:WorkoutGuideWorkoutGuide  5      *    $WorkoutGuide.AddVideoToWorkoutWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ,localclr-namespace:WorkoutGuide     5        � Title$"  Video zu Workout hinzufügen��5      $	��132��6)   $	��511��66    � 
ResizeMode$ NoResize=�6B    � ShowInTaskbar .   � Topmost .  � WindowStartupLocation$ CenterScreen=�6}    � DataContext 5         WorkoutGuide.ViewModel  5   
   .��5	      � $��10,30q�6    � ColumnDefinitions 5
   
   �� 5      $
��147* �6   6.   �� 5      $
��177* �6   6.   �� 5      $
��161* �6   6.   .+�5   
   �� $��0��6   $��15(�5      $��Center=�5      $��30��6+   $	��0,7q�67   .��5      Workout auswählen:6%   �� 5   
   $��1��6   $	��200��5      $
��Left=�5      $��15(�5      $��Center=�5      $��Center=�5      $��2��67   ��5      �� *AllWorkouts.Workouts+ �� SelectedItem 5      �� *ChosenWorkout+ �� Mode$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=�5   M   �� 5   
   $��2��6   $��15(�5      $	��100��5      $��Right=�5      $��Hinzufügen��5      ��5       �� *AddVideoToWorkoutCommand+
 � ToolTip
 5!      _� 5"      .��5#      �� .+�5$      �� $
��Bold"�6$   Zu Workout hinzufügen6M   �� 5%      20Fügt das Video zum ausgewählten Workout hinzu.6T   5&      5'      5)      5*      5+      