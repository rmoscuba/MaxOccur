# Max occurrencies in a string
Characterr that occurs the most in a given string

* Return the character that occurs the most in a given string. 
If more than one character  occurs the same number of times, 
then return all the characters in a comma-separated string.

There are two versions of the function a pure LINQ version:
```
public static string maxOccurPureLINQ(string s);
```
And the iterative version:
```
public static string maxOccur(string s);
```

# Day wage calculation

A function that calculates the payment associated with a job considering both regular and overtime hours.
* Work from 8 am to 6 pm regular hours.
* Anything outside of that time frame is paid at an overtime rate.

The function receives 4 values:
* Start time of the worker (24-hour notation) i.e: "HH:MM"
* End time of the worker (same format)
* Hourly wage (regular hours)
* Multiplication factor for overtime hours.

NOTE: A worker can start working at any time of the day and can work any number of hours in a day.

The function should return:
* The total salary for the day.
