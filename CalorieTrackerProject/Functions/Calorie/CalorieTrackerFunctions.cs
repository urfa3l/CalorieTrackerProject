using CalorieTracker;
using System;

public class CalorieTracker
{
    private DatabaseHelper dbManager;
    private CalorieTrackerModel trackerModel;

    public CalorieTracker(DatabaseHelper dbManager, CalorieTrackerModel trackerModel)
    {
        this.dbManager = dbManager;
        this.trackerModel = trackerModel;
    }

    public void AddCalorieTracker()
    {
        string query = "INSERT INTO CalorieTracker (UserID, CalorieIntakeTotal, CalorieOutputTotal, BMR, Date) VALUES (@UserID, @CalorieIntakeTotal, @CalorieOutputTotal, @BMR, @Date)";
        dbManager.ExecuteNonQuery(query, command =>
        {
            command.Parameters.AddWithValue("@UserID", trackerModel.UserID);
            command.Parameters.AddWithValue("@CalorieIntakeTotal", trackerModel.CalorieIntakeTotal);
            command.Parameters.AddWithValue("@CalorieOutputTotal", trackerModel.CalorieOutputTotal);
            command.Parameters.AddWithValue("@BMR", trackerModel.BMR);
            command.Parameters.AddWithValue("@Date", trackerModel.Date);
        });
    }
}