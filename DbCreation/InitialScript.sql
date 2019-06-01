Drop database IF exists weather_AUS;

Create database weather_AUS;
GO
use weather_AUS;
Drop table IF EXISTS dbo.weather_Staging;
Drop table IF EXISTS dbo.weather;

Create Table dbo.weather_Staging (
	Date date,
	Location varchar(40),
	MinTemp float,
	MaxTemp float,
	Rainfall float,
	Evaporation float,
	Sunshine float,
	WindGustDir varchar(10),
	WindGustSpeed int,
	WindDir9am varchar(10),
	WindDir3pm varchar(10),
	WindSpeed9am int,
	WindSpeed3pm int,
	Humidity9am int,
	Humidity3pm int,
	Pressure9am float,
	Pressure3pm float,
	Cloud9am int,
	Cloud3pm int,
	Temp9am float,
	Temp3pm float,
	RainToday varchar(10),
	RISK_MM float,
	RainTomorrow varchar(10)
	);

	Create Table dbo.weather (
    Id int primary Key IDENTITY(1,1),
	Date date,
	Location varchar(40),
	MinTemp float,
	MaxTemp float,
	Rainfall float,
	Evaporation float,
	Sunshine float,
	WindGustDir varchar(10),
	WindGustSpeed int,
	WindDir9am varchar(10),
	WindDir3pm varchar(10),
	WindSpeed9am int,
	WindSpeed3pm int,
	Humidity9am int,
	Humidity3pm int,
	Pressure9am float,
	Pressure3pm float,
	Cloud9am int,
	Cloud3pm int,
	Temp9am float,
	Temp3pm float,
	RainToday varchar(10),
	RISK_MM float,
	RainTomorrow varchar(10)
	);

Bulk INSERT dbo.weather_Staging
	--type your weatherAUS file location here
	FROM 'D:\Studia\Semestr 6\KSR\LinguisticSummarizationOfDatabase\DbCreation'
	WITH
	(
	FIRSTROW = 2,
	FIELDTERMINATOR = ',',
	ROWTERMINATOR = '0x0a'
	);

Insert into dbo.weather 
select * from dbo.weather_Staging where
MinTemp is not null and
MaxTemp is not null and
Rainfall is not null and
Evaporation is not null and
Sunshine is not null and
WindGustDir is not null and
WindGustSpeed is not null and
WindDir9am is not null and
WindDir3pm is not null and
WindSpeed9am is not null and
WindSpeed3pm is not null and
Humidity9am is not null and
Humidity3pm is not null and
Pressure9am is not null and
Pressure3pm is not null and
Cloud9am is not null and
Cloud3pm is not null;

Drop table dbo.weather_Staging;
