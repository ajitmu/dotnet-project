using Microsoft.Data.SqlClient;

namespace SmartCampusPortal.DAL
{
    // Creates the database, the table and some sample data if they do not exist.
    public static class DatabaseInitializer
    {
        public static void Initialize(string connectionString)
        {
            var csb = new SqlConnectionStringBuilder(connectionString);
            string dbName = csb.InitialCatalog;

            // 1. Create database (connect to master first)
            csb.InitialCatalog = "master";
            using (var con = new SqlConnection(csb.ConnectionString))
            {
                con.Open();
                string sql = "IF DB_ID(N'" + dbName + "') IS NULL CREATE DATABASE [" + dbName + "];";
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            // 2. Create table + seed data
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();

                string createTable = @"
IF OBJECT_ID(N'dbo.ServiceRequests', N'U') IS NULL
CREATE TABLE dbo.ServiceRequests
(
    RequestId   INT IDENTITY(1,1) PRIMARY KEY,
    StudentName NVARCHAR(100) NOT NULL,
    Department  NVARCHAR(100) NOT NULL,
    ServiceType NVARCHAR(50)  NOT NULL,
    Description NVARCHAR(500) NOT NULL,
    Priority    NVARCHAR(20)  NOT NULL DEFAULT 'Medium',
    Status      NVARCHAR(20)  NOT NULL DEFAULT 'Pending',
    RequestDate DATETIME      NOT NULL DEFAULT GETDATE()
);";
                using (var cmd = new SqlCommand(createTable, con))
                {
                    cmd.ExecuteNonQuery();
                }

                string seed = @"
IF NOT EXISTS (SELECT 1 FROM dbo.ServiceRequests)
INSERT INTO dbo.ServiceRequests (StudentName, Department, ServiceType, Description, Priority, Status)
VALUES
 (N'Rahul Sharma', N'Computer Engineering', N'IT Support', N'Wi-Fi not working in Lab 3', N'High', N'Pending'),
 (N'Priya Patel', N'Information Technology', N'Library', N'Need access to e-journals', N'Low', N'In Progress'),
 (N'Aarav Mehta', N'Mechanical Engineering', N'Maintenance', N'Projector not working in Room 204', N'Medium', N'Completed');";
                using (var cmd = new SqlCommand(seed, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
