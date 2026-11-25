using Microsoft.Data.SqlClient;
using SmartCampusPortal.Models;

namespace SmartCampusPortal.DAL
{
    // ADO.NET data access layer for the ServiceRequests table
    public class ServiceRequestDAL
    {
        private readonly string _connectionString;

        private const string SelectColumns =
            "RequestId, StudentName, Department, ServiceType, Description, Priority, Status, RequestDate";

        public ServiceRequestDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<ServiceRequest> GetAll()
        {
            var list = new List<ServiceRequest>();

            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(
                "SELECT " + SelectColumns + " FROM ServiceRequests ORDER BY RequestDate DESC, RequestId DESC", con))
            {
                con.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add(Map(rd));
                    }
                }
            }

            return list;
        }

        public ServiceRequest GetById(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(
                "SELECT " + SelectColumns + " FROM ServiceRequests WHERE RequestId = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                    {
                        return Map(rd);
                    }
                }
            }

            return null;
        }

        // Returns the new RequestId
        public int Insert(ServiceRequest request)
        {
            const string sql = @"
INSERT INTO ServiceRequests (StudentName, Department, ServiceType, Description, Priority, Status, RequestDate)
OUTPUT INSERTED.RequestId
VALUES (@StudentName, @Department, @ServiceType, @Description, @Priority, 'Pending', GETDATE())";

            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql, con))
            {
                cmd.Parameters.AddWithValue("@StudentName", request.StudentName);
                cmd.Parameters.AddWithValue("@Department", request.Department);
                cmd.Parameters.AddWithValue("@ServiceType", request.ServiceType);
                cmd.Parameters.AddWithValue("@Description", request.Description);
                cmd.Parameters.AddWithValue("@Priority", request.Priority ?? "Medium");

                con.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        // Returns true when a row was updated
        public bool UpdateStatus(int id, string status)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(
                "UPDATE ServiceRequests SET Status = @Status WHERE RequestId = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // Returns true when a row was deleted
        public bool Delete(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(
                "DELETE FROM ServiceRequests WHERE RequestId = @Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        private static ServiceRequest Map(SqlDataReader rd)
        {
            return new ServiceRequest
            {
                RequestId = Convert.ToInt32(rd["RequestId"]),
                StudentName = rd["StudentName"].ToString(),
                Department = rd["Department"].ToString(),
                ServiceType = rd["ServiceType"].ToString(),
                Description = rd["Description"].ToString(),
                Priority = rd["Priority"].ToString(),
                Status = rd["Status"].ToString(),
                RequestDate = Convert.ToDateTime(rd["RequestDate"])
            };
        }
    }
}
