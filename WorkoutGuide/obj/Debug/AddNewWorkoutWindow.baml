   M S B A M L   `   `   `  ����G  CWorkoutGuide, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:WorkoutGuideWorkoutGuide  5      &     WorkoutGuide.AddNewWorkoutWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ,localclr-namespace:WorkoutGuide     5      -   5          �Name$  AddNewWorkoutWindow��5       � Title$ Workout hinzufügen��5      $	��281��6    $	��409��6-    � 
ResizeMode$ NoResize=�5       � ShowInTaskbar .   � Topmost .  � WindowStartupLocation$ CenterScreen=�6@    � DataContext 5         WorkoutGuide.ViewModel  5	   
    � 	Resources 5         
LabelStyle �� 
TargetType	 �� Property
 �� Value  TextboxStyle  ButtonStyle��%9  &	        &	 z     &	     �� 5   
   " ��5      .�5      �� 	 � ��$
 15(�6)   66   �� 5      	 � ��$
 10q�6'   64   5      �� 5   
   " ��6%   .�5      �� 	 � ��$
 15(�6)   66   �� 5      	 � ��$
 15q�6'   64   �� 5      	 � ��$
 Center=�62   6C   5      �� 5   
   " ��5      .�5      �� 	 � ��$
 15(�6)   66   �� 5      	 � ��$
 15,10q�6'   67   �� 5      	 � ��$
 Right=�64   6D   �� 5      	 � ��$
 Center=�62   6C   �� 5       	 � ��$
 3q�6(   64   5!      5#      .��5%      �  � RowDefinitions 5&   
   �� 5'      $��* �6   6)   �� 5(      $��2* �6   6*   �� 5)      $��* �6   6)    � ColumnDefinitions 5+   
   �� 5,      $��* �6   6+   �� 5-      $��2* �6   6,   .+�5/   
   �� $��0��6   $��0��6   #��[  50      .��64   Titel:6<   �� 51   
   $��1��6   $��0��6   #��[  52      .��64   Beschreibung:6C   �� 53   
   $��0��6   $��1��6   #��[ 54      ��55      �� *NewWorkout.WorkoutTitle+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6o   �� 56   
   $��1��6   $��1��6   $	��Top=�57      $��90��58      #��[ 59       �� TextWrapping$
 Wrap=�5;       �� AcceptsReturn . ��5:      �� *NewWorkout.WorkoutDescription+$ TwoWay=�$ PropertyChanged=�5<   )   �� 5=   
   $��2��6   $��1��6   #��[ 5>       �� 	IsDefault . ��5?      �� *AddWorkoutCommand+.��5@   "   Workout hinzufügen66   5A      5B      