using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Xml.Linq;
using task3.Data.Mapper;
using task3.dto;
using task3.Models;




namespace task3.Data
{
    public class ADOdotNET
    {
        private readonly SqlConnection connection;
        private readonly DeviceMapper _deviceMapper;
        private readonly ClientMapper _clientMapper;
        private readonly PhoneNumberMapper _phoneNumberMapper;
        private readonly PhoneNumberMapperdto _phoneNumberMapperdto;

        public ADOdotNET(SqlConnection connection, DeviceMapper deviceMapper, ClientMapper clientMapper
            , PhoneNumberMapper phoneNumberMapper, PhoneNumberMapperdto phoneNumberMapperdto
            )
        {
            this.connection = connection;
            _deviceMapper = deviceMapper;
            _clientMapper = clientMapper;
            _phoneNumberMapper = phoneNumberMapper;
            _phoneNumberMapperdto = phoneNumberMapperdto;
        }



        public List<Device> GetAllDevices()
        {
            connection.Open();
            SqlCommand command = new SqlCommand("GetAllDevices", connection);
            command.CommandType = CommandType.StoredProcedure;

            var result = command.ExecuteReader();
            List<Device> devices = new List<Device>();
            while (result.Read())
            {
                int _id = result.GetInt32(0);
                string _name = result.GetString(1);
                Device temp = _deviceMapper.Map(_id, _name);
                devices.Add(temp);
            }
            connection.Close();
            return devices;
        }

        public List<Device> GetFilteredDevices(string name)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("GetFilteredDevices", connection);
            command.Parameters.AddWithValue("@Name", name);
            command.CommandType = CommandType.StoredProcedure;

            var result = command.ExecuteReader();
            List<Device> devices = new List<Device>();
            while (result.Read())
            {
                int _id = result.GetInt32(0);
                string _name = result.GetString(1);
                Device temp = _deviceMapper.Map(_id, _name);
                devices.Add(temp);
            }
            connection.Close();
            return devices;
        }





        public Device AddDevice(Device device)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("AddDevice", connection);
            var name = device.Name;
            command.Parameters.AddWithValue("@Name", name);
            command.CommandType = CommandType.StoredProcedure;
            var result = command.ExecuteScalar();
            device.Id = Convert.ToInt32(result);
            connection.Close();
            return device;
        }

        public Device UpdateDevice(Device device)
        {
            connection.Open();
            SqlCommand command = new SqlCommand("UpdateDevice", connection);
            var name = device.Name;
            var id = device.Id;
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Id", id);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            connection.Close();
            return device;
        }

        public bool DeleteDevice(int id)
        {
            using (var command = new SqlCommand("DeleteDevice", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    return (bool)command.ExecuteScalar();   // true = انحذف، false = ممنوع
                }
                finally
                {
                    connection.Close();
                }
            }
        }


        // end of device 




        //GetAllClients


        public List<Client> GetAllClients()
        {
            connection.Open();
            SqlCommand command = new SqlCommand("GetAllClients", connection);
            command.CommandType = CommandType.StoredProcedure;
            var result = command.ExecuteReader();
            List<Client> clients = new List<Client>();
            while (result.Read())
            {
                int _id = result.GetInt32(0);
                string _name = result.GetString(1);
                int _type = result.GetInt32(2);
                DateTime? _date = result.IsDBNull(3)
                 ? null
                : result.GetDateTime(3);
                ClientType type;
                if (_type == 0) type = ClientType.Individual; else type = ClientType.Organization;
                Client temp = _clientMapper.Map(_id, _name, type, _date);
                clients.Add(temp);
            }
            connection.Close();
            return clients;

        }
        //GetFilteredClients

        public List<Client> GetFilteredClients(string? name, ClientType? type)
        {
            var clients = new List<Client>();

            using (var command = new SqlCommand("GetFilteredClients", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value =
                    string.IsNullOrWhiteSpace(name) ? DBNull.Value : name.Trim();

                command.Parameters.Add("@Type", SqlDbType.Int).Value =
                    type.HasValue ? (int)type.Value : DBNull.Value;

                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string clientName = reader.GetString(1);
                            var clientType = (ClientType)reader.GetInt32(2);
                            DateTime? date = reader.IsDBNull(3) ? null : reader.GetDateTime(3);

                            clients.Add(_clientMapper.Map(id, clientName, clientType, date));
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }

            return clients;
        }

        //AddClient
        public Client AddClient(Client client)
        {

            string _name = client.Name;
            ClientType clientType = client.Type;
            DateTime? _date = client.BirthDate;
            int _type;
            if (clientType == ClientType.Individual)
                _type = 0;
            else
                _type = 1;

            connection.Open();
            SqlCommand command = new SqlCommand("AddClient", connection);

            command.Parameters.AddWithValue("@Name", _name);
            command.Parameters.AddWithValue("@Type", _type);
            command.Parameters.AddWithValue("@BirthDate", _date ?? (object)DBNull.Value);

            command.CommandType = CommandType.StoredProcedure;
            var result = command.ExecuteScalar();
            client.Id = Convert.ToInt32(result);
            connection.Close();
            return client;
        }



        //UpdateClient

        public Client UpdateClient(Client client)
        {
            int _id = client.Id;
            string _name = client.Name;
            ClientType clientType = client.Type;
            DateTime? _date = client.BirthDate;
            int _type;
            if (clientType == ClientType.Individual)
                _type = 0;
            else
                _type = 1;
            connection.Open();
            SqlCommand command = new SqlCommand("UpdateClient", connection);
            
            command.Parameters.AddWithValue("@Name", _name);
            command.Parameters.AddWithValue("@Id", _id);
            command.Parameters.AddWithValue("@Type", _type);
            command.Parameters.AddWithValue("@BirthDate", _date ?? (object)DBNull.Value);
            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            connection.Close();
            return client;
        }

        //DeleteClient

        public bool DeleteClient(int id)
        {
            using (var command = new SqlCommand("DeleteClient", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    return (bool)command.ExecuteScalar();   // true = انحذف، false = ممنوع
                }
                finally
                {
                    connection.Close();
                }
            }
        }




        // end of Client


        //GetAllPhoneNumbers
        public List<PhoneNumberdto> GetAllPhoneNumbers()
        {
            connection.Open();

            SqlCommand command = new SqlCommand("GetAllPhoneNumbers", connection);
            command.CommandType = CommandType.StoredProcedure;

            var result = command.ExecuteReader();

            List<PhoneNumberdto> PhoneNumberdto = new List<PhoneNumberdto>();

            while (result.Read())
            {
                int _id = result.GetInt32(0);
                string _number = result.GetString(1);
                int _deviceId = result.GetInt32(2);
                string _deviceName = result.GetString(3);

                PhoneNumberdto temp =
                    _phoneNumberMapperdto.Map(_id, _number, _deviceId, _deviceName);

                PhoneNumberdto.Add(temp);
            }

            result.Close();
            connection.Close();

            return PhoneNumberdto;
        }


        // GetFilteredPhoneNumbers
        public List<PhoneNumber> GetFilteredPhoneNumbers(
    string? number,
    int? deviceId)
        {
            connection.Open();

            SqlCommand command =
                new SqlCommand("GetFilteredPhoneNumbers", connection);

            command.Parameters.AddWithValue(
                "@Number",
                string.IsNullOrEmpty(number) ? (object)DBNull.Value : number);

            command.Parameters.AddWithValue(
                "@DeviceId",
                deviceId.HasValue ? (object)deviceId.Value : DBNull.Value);

            command.CommandType = CommandType.StoredProcedure;

            var result = command.ExecuteReader();

            List<PhoneNumber> phoneNumbers = new List<PhoneNumber>();

            while (result.Read())
            {
                int _id = result.GetInt32(0);
                string _number = result.GetString(1);
                int _deviceId = result.GetInt32(2);

                PhoneNumber temp =
                    _phoneNumberMapper.Map(_id, _number, _deviceId);

                phoneNumbers.Add(temp);
            }

            connection.Close();

            return phoneNumbers;
        }


        // AddPhoneNumber
        public PhoneNumber AddPhoneNumber(PhoneNumber phoneNumber)
        {
            connection.Open();

            SqlCommand command =
                new SqlCommand("AddPhoneNumber", connection);

            command.Parameters.AddWithValue("@Number", phoneNumber.Number);
            command.Parameters.AddWithValue("@DeviceId", phoneNumber.DeviceId);

            command.CommandType = CommandType.StoredProcedure;

            var result = command.ExecuteScalar();

            phoneNumber.Id = Convert.ToInt32(result);

            connection.Close();

            return phoneNumber;
        }


        //UpdatePhoneNumber
        public PhoneNumber UpdatePhoneNumber(PhoneNumber phoneNumber)
        {
            connection.Open();

            SqlCommand command =
                new SqlCommand("UpdatePhoneNumber", connection);

            command.Parameters.AddWithValue("@Id", phoneNumber.Id);
            command.Parameters.AddWithValue("@Number", phoneNumber.Number);
            command.Parameters.AddWithValue("@DeviceId", phoneNumber.DeviceId);

            command.CommandType = CommandType.StoredProcedure;

            command.ExecuteNonQuery();

            connection.Close();

            return phoneNumber;
        }


        // DeletePhoneNumber

        public bool DeletePhoneNumber(int id)
        {
            using (var command = new SqlCommand("DeletePhoneNumber", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    return (bool)command.ExecuteScalar();   // true = انحذف، false = ممنوع
                }
                finally
                {
                    connection.Close();
                }
            }
        }



        //////////////////
        /// PhoneNumberReservation


        public List<PhoneNumberReservation> GetAllReservations()
        {
            var list = new List<PhoneNumberReservation>();
            using (var command = new SqlCommand("GetAllReservations", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                
                try
                {
                    OpenConnection();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new PhoneNumberReservation
                            {
                                Id = reader.GetInt32(0),
                                ClientId = reader.GetInt32(1),
                                ClientName = reader.GetString(2),
                                PhoneNumberId = reader.GetInt32(3),
                                PhoneNumber = reader.GetString(4),
                                Bed = reader.GetDateTime(5),
                                Eed = reader.IsDBNull(6) ? null : reader.GetDateTime(6)
                            });
                        }
                    }
                }
                finally { connection.Close(); }
            }
            return list;
        }


        public List<PhoneNumberReservation> GetFilteredReservations(int? clientId, int? phoneNumberId)
        {
            var list = new List<PhoneNumberReservation>();

            using (var command = new SqlCommand("GetFilteredReservations", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@ClientId", SqlDbType.Int).Value =
                    clientId.HasValue ? clientId.Value : DBNull.Value;

                command.Parameters.Add("@PhoneNumberId", SqlDbType.Int).Value =
                    phoneNumberId.HasValue ? phoneNumberId.Value : DBNull.Value;

                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new PhoneNumberReservation
                            {
                                Id = reader.GetInt32(0),
                                ClientId = reader.GetInt32(1),
                                ClientName = reader.GetString(2),
                                PhoneNumberId = reader.GetInt32(3),
                                PhoneNumber = reader.GetString(4),
                                Bed = reader.GetDateTime(5),
                                Eed = reader.IsDBNull(6) ? null : reader.GetDateTime(6)
                            });
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }

            return list;
        }
        public List<PhoneNumber> GetFreePhoneNumbers()
        {
            var list = new List<PhoneNumber>();
            using (var command = new SqlCommand("GetFreePhoneNumbers", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                try
                {
                    OpenConnection();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            list.Add(_phoneNumberMapper.Map(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
                    }
                }
                finally { connection.Close(); }
            }
            return list;
        }

        
        public int ReservePhoneNumber(int clientId, int phoneNumberId)
        {
            using (var command = new SqlCommand("ReservePhoneNumber", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ClientId", SqlDbType.Int).Value = clientId;
                command.Parameters.Add("@PhoneNumberId", SqlDbType.Int).Value = phoneNumberId;
                try
                {
                    OpenConnection();
                    return Convert.ToInt32(command.ExecuteScalar());
                }
                finally { connection.Close(); }
            }
        }

        
        public bool EndReservation(int reservationId)
        {
            using (var command = new SqlCommand("EndReservation", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Id", SqlDbType.Int).Value = reservationId;
                try
                {
                    OpenConnection();
                    return (bool)command.ExecuteScalar();
                }
                finally { connection.Close(); }
            }
        }

        public List<PhoneNumberReservation> GetActiveReservationsByClient(int clientId)
        {
            var list = new List<PhoneNumberReservation>();
            using (var command = new SqlCommand("GetClientActiveReservations", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@ClientId", SqlDbType.Int).Value = clientId;
                try
                {
                    OpenConnection();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new PhoneNumberReservation
                            {
                                Id = reader.GetInt32(0),
                                ClientId = reader.GetInt32(1),
                                ClientName = reader.GetString(2),
                                PhoneNumberId = reader.GetInt32(3),
                                PhoneNumber = reader.GetString(4),
                                Bed = reader.GetDateTime(5),
                                Eed = reader.IsDBNull(6) ? null : reader.GetDateTime(6)
                            });
                        }
                    }
                }
                finally { connection.Close(); }
            }
            return list;
        }

        private void OpenConnection()
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
        }

    } // end of ADOdotNET




}