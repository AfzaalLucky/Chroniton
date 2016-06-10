## Synopsis

A library for running tasks(jobs) on schedules. It supports:
	* Strongly typed jobs
	* Custom schedules
	* Running jobs on multiple schedules
	* Multiple jobs on a single schedule.
	* Limiting the number of threads on which work is done
	* Managing behaviors of jobs which run beyond their next scheduled time
	* Dependency Injection initialization

## Code Example

	static void Main(string[] args)
	{
		ISingularityFactory factory = new SingularityFactory();
		ISingularity singularity = factory.GetSingularity();

		var job = new SimpleParameterizedJob<string>(
			anything => Task.Run(()=> Console.WriteLine(anything)));

		var schedule = new EveryXTimeSchedule(TimeSpan.FromSeconds(1));

		var scheduledJob = singularity.ScheduleParameterizedJob(
			schedule, job, "Hello World", true); //starts immediately

		var startTime = DateTime.UtcNow.Add(TimeSpan.FromSeconds(5));

		var scheduledJob2 = singularity.ScheduleParameterizedJob(
			schedule, job, "Hello World 2", startTime);

		singularity.Start();

		Thread.Sleep(10 * 1000);

		singularity.StopScheduledJob(scheduledJob);

		Thread.Sleep(5 * 1000);
		
		singularity.Stop();

		Console.ReadKey();
	}

## Motivation

This project was inspired for the need to have a strongly typed .NET solution for running tasks on schedules. 

## Installation

Provide code examples and explanations of how to get the project.

## Contributors

Created by : Leonard Sperry
leosperry@outlook.com

## License

A short snippet describing the license (MIT, Apache, etc.)

