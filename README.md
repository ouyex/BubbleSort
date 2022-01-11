# Bubble Sort
A basic C# console application for the bubble sort algorithm.

## Download
Download from the GitHub repository releases [here](https://github.com/Ouyex/BubbleSort/releases/tag/Releases).

## Info
- Size of sorted array is defined by the user.
- Sorted arrays are randomly generated between minimum and maximum values, which are defined by the user.
- This program does NOT support manual arrays, only randomized ones.
- Due to the bubble sort algorithm itself, higher arrays can take extremely long to calculate. Refer to the [Speed Tests](#sorting-speed-tests) before inputting custom values.
- Exported files run through the "write_sort" and "write_unsort" commands are saved in the same directory as the program itself, as "sorted.txt" and "unsorted.txt" respectively.
- "force_fetch" and "force_unfetch" commands are not required to run manually, commands that require the strings of the arrays are fectched automatically. They are simply there for debug purposes, or to simultaneously get both the sorted and unsorted array strings to prevent waiting later.

___

### Sorting Speed Tests
- Sorting tests were ran on an Intel i7-8565U.
- Values can heavily vary depending on both CPU speed and how the unsorted array is generated.
- These tests were run in a debug build, actual times will sometimes be less than 50% of the listed times.
- Use this only as a rough relative guide, not as real-world times.

```
 Sizes        Time           Passes
_______   _____________  ______________
500,000 : 1156  seconds (499,430 passes)
400,000 :  645  seconds (398,479 passes)
300,000 :  363  seconds (299,542 passes)
200,000 :  161  seconds (199,376 passes)
100,000 :   41  seconds (99,615  passes)
50,000  :   11  seconds (49,751  passes)
25,000  :  2.8  seconds (24,819  passes)
10,000  :  0.5  seconds (9,878   passes)
1,000   : 0.02  seconds (938     passes)
```
___

### Known Issues
- Windows Notepad may switch to a different type of text encoding on some files, which can cause numbers to show up as foreign characters. An application like [Notepad++](https://notepad-plus-plus.org/downloads/) is strongly recommended for viewing output files.
