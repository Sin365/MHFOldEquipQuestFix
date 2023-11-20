##[Cn]

适用于，MHF老版本的防具任务，从MHFZ的任务，修正为Erupe支持读取的格式。

使得MHF老版本防具任务可玩

顺带，Malcky看到代码后，添加了任务SR/AI的偏移量处理，感谢他的追加。

使用方法：

拷贝需要批处理的任务文件到Input，

启动程序，按提示，确认。

然后会转换所有Input文件夹的文件到Out文件夹。

##[En] 

Here is a tool for batch repairing preset equipment type Quest Files

Enable files to support Erupe in FW5 mode

Can be completed in bulk.

I have been verifying on my Chinese FW5 server for over a month and there are no issues using it. I see Forward Fivers server, fix this type of quest files due to its large volume. I have shared the tool source code with Cruise, Forward Fivers can quickly repair quest files for players to play.

I hope to contribute my humble efforts.

And after Malskyor saw the code, he added SR/AI data offset based on this code.

Malcky changed the code slightly to include moving the SR/AI flag bytes. This is his added version. I will soon join the github repo as well

Converter tool that fixes different equipment and variant/SR lock structs from MHFZZ to older versions (so far we are using it for MHFW5).

How to Use:

1 - Copy and paste all MHFZZ quests inside the "Input" folder.

2 - Run "MHFOldEquipQuestFix.exe" and wait for it to read all the files.

3 - Type "y" and press enter after it shows the amount of quest files inside the folder.

4 - After finished all the resulting new files will be inside the "Out" folder, copy them to your older MHF server.

5 - Enjoy!
