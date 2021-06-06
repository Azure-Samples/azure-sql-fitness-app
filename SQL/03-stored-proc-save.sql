create or alter procedure web.post_trainingsession 
@json nvarchar(max)
as

declare @recordedOn varchar(50) = json_value(@json, '$.RecordedOn');
declare @workoutType varchar(50) = json_value(@json, '$.WorkoutType');
declare @steps int = json_value(@json, '$.Steps');
declare @distance int = json_value(@json, '$.Distance');
declare @duration int = json_value(@json, '$.Duration');
declare @calories int = json_value(@json, '$.Calories');
declare @userId varchar(32) = json_value(@json, '$.UserId')

set xact_abort on
set transaction isolation level snapshot;  
begin tran 

    insert into dbo.TrainingSession 
        (RecordedOn, [Type], Steps, Distance, Duration, Calories, UserId)
    values 
        (@recordedOn, @workoutType, @steps, @distance, @duration, @calories, @userId)
    
    
commit tran