   M S B A M L   `   `   `  ����F  BGUI_Projekt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:GUI_ProjektGUI_Projekt  5          GUI_Projekt.HighscorePage   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      +localclr-namespace:GUI_Projekt     5        N� Title$  HighscorePage��5	      .��5      � $	��800��6   $	��400��6   
��� ���.+�5   
   �� $��
Highscores��6   $
��Left=�6%   $	��Top=�6@   $��
297,27,0,0q�6X   $��Bahnschrift Light)�6l   $��36(�6�   �� 5   
   -   5        $��Zurück��5      $��Center=�6"   $��632,318,18,22q�6?   $��Center=�6V   $	��150��6q   $��60��6}   $��36(�6�   $��Bahnschrift Light)�6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ HighscoresDataGrid��5   t   $
��Left=�6   $	��271��6.   $��151,107,0,0q�6;   $	��Top=�6P   $	��449��6h      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$
 Rang��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 63   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *Rang+  5      $ Spieler��6%   $ * 66    6@   �� *Name+  5      $ Level��6%   $ * 64    6>   �� *Level+  5      $ Punkte��6%   $ * 65    6?   �� *	Highscore+ � 	Resources 5      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property	 �� Value��%�   '        �� 5      "  6   .�5      ��  � ��
	 �   �6C   �� 5       � ��
	 �����6C   �� 5       � ��$	 0,0,1,2q�68   6J   �� 5        � ��
	 �����6D   5!      5#      5$      5*      5,      