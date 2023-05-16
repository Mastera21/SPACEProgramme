# SPACEProgramme
This is program where we calculate when is the best time and conditions to launch rocket.\
This project provides functionality that allows us to read from a ".csv" file or provide custom input, store all the information in a new ".csv" and then send the file as an attachment to an email.

## Task details
You are preparing for a SPACE mission.\
You are in the SPACE mission control centre.\
Your task is to calculate which is the most appropriate day for the SPACE shuttle launch based on the weather conditions.\
You have the weather forecast for the first half of July and the weather criteria for a successful shuttle launch.

## Criteria for the weather conditions for a rocket launch are:
 - Temperature between 2 and 31 degrees Celsius;
 - Wind speed no more than 10m/s (the lower the better);
 - Humidity less than 60% (the lower the better);
 - No precipitation;
 - No lightings;
 - No cumulus or nimbus clouds
 - Average value
 - Max value
 - Min value
 - Median value
 - Most appropriate launch day parameter value

## Overview of the "DataBase.csv" file we are reading information from.
![Acrobat_DUyxXmhiWX](https://github.com/Mastera21/SPACEProgramme/assets/68899725/59797f96-3e55-4bc6-9e0e-c07a7005a773)


# Functionality
Functionalities that the project gives.

- 1 Read custom input (We are typing what parameter we want to store in our ".csv" file).
- 2 Read from database (Reading from ".csv" file that already has information).
- 3 Storing all information in "WeatherReport.csv" file. (location: "bin/Debug/net7.0/WeatherReport.csv")
- 4 Send "WeatherReport.csv" file to email address that we want. 

## Development:
- Developed on Windows 11.
- Visual Studio 2022.


## Using tools:
- C#.
- SMTP (to establish connection and send the file as attachment to the email).
