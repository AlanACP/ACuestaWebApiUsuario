using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static (bool,string) Add(ML.Usuario usuario)
        {
			try
			{
				using (SqlConnection context = new SqlConnection(DL.Connection.GetConnectionString()))
				{
					string query = "INSERT INTO Usuario Values(@Nombre, @ApellidoPaterno, @ApellidoMaterno, @Edad, @Email)";

					SqlCommand command = new SqlCommand();
					command.CommandText = query;
					command.Connection = context;

					SqlParameter[] parameters = new SqlParameter[5];

					parameters[0] = new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar);
					parameters[0].Value = usuario.Nombre;

					parameters[1] = new SqlParameter("@ApellidoPaterno", System.Data.SqlDbType.VarChar);
					parameters[1].Value = usuario.ApellidoPaterno;

					parameters[2] = new SqlParameter("@ApellidoMaterno", System.Data.SqlDbType.VarChar);
					parameters[2].Value = usuario.ApellidoMaterno;

					parameters[3] = new SqlParameter("@Edad", System.Data.SqlDbType.Int);
					parameters[3].Value = usuario.Edad;

					parameters[4] = new SqlParameter("@Email", System.Data.SqlDbType.VarChar);
					parameters[4].Value = usuario.Email;

					command.Parameters.AddRange(parameters);
					command.Connection.Open();

					int rowsAffected = command.ExecuteNonQuery();

					if (rowsAffected > 0)
					{
						return (true, "Usuario agregado correctamente");
					}
					else
					{
						return (false, "Ocurrio un error al agregar el usuario");
					}

				}
			}
			catch (Exception ex)
			{
				return (false, ex.Message);
			}
        }

		public static (bool,string) Update(ML.Usuario usuario)
		{
			try
			{
				using (SqlConnection context = new SqlConnection(DL.Connection.GetConnectionString()))
				{
					string query = "UPDATE Usuario SET Nombre = @Nombre, ApellidoPaterno = @ApellidoPaterno, ApellidoMaterno = @ApellidoMaterno, Edad = @Edad, Email = @Email WHERE IdUsuario = @IdUsuario";

					SqlCommand command = new SqlCommand();
					command.Connection = context;
					command.CommandText = query;

					SqlParameter[] parameters = new SqlParameter[6];

                    parameters[0] = new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar);
                    parameters[0].Value = usuario.Nombre;

                    parameters[1] = new SqlParameter("@ApellidoPaterno", System.Data.SqlDbType.VarChar);
                    parameters[1].Value = usuario.ApellidoPaterno;

                    parameters[2] = new SqlParameter("@ApellidoMaterno", System.Data.SqlDbType.VarChar);
                    parameters[2].Value = usuario.ApellidoMaterno;

                    parameters[3] = new SqlParameter("@Edad", System.Data.SqlDbType.Int);
                    parameters[3].Value = usuario.Edad;

                    parameters[4] = new SqlParameter("@Email", System.Data.SqlDbType.VarChar);
                    parameters[4].Value = usuario.Email;

					parameters[5] = new SqlParameter("@IdUsuario" ,System.Data.SqlDbType.Int);
					parameters[5].Value = usuario.IdUsuario;

					command.Parameters.AddRange(parameters);
					command.Connection.Open();

					int rowsAffected = command.ExecuteNonQuery();

					if (rowsAffected > 0 )
					{
						return (true, "Usuario actualizado correctamente");
					}
					else
					{
						return (false, "Ocurrio un problema al actualizar el usuario");
					}
                }
			}
			catch (Exception ex)
			{
				return (false, ex.Message);
			}
		}

		public static (bool,string) Delete(int IdUsuario)
		{
			try
			{
				using (SqlConnection context = new SqlConnection(DL.Connection.GetConnectionString()))
				{
					var query = "DELETE FROM Usuario WHERE IdUsuario = @IdUsuario";

					SqlCommand command = new SqlCommand();
					command.Connection = context;
					command.CommandText = query;

					SqlParameter[] parameter = new SqlParameter[1];

					parameter[0] = new SqlParameter("@IdUsuario", System.Data.SqlDbType.Int);
					parameter[0].Value = IdUsuario;
					
					command.Parameters.AddRange(parameter);
					command.Connection.Open();

					int rowsAffected = command.ExecuteNonQuery();

					if (rowsAffected > 0 )
					{
						return (true, "Usuario eliminado correctamente");
					}
					else
					{
						return (false, "Error al eliminar el usuario");
					}

				}
			}
			catch (Exception ex)
			{
				return (false, ex.Message);
			}
		} 

		public static (bool,string,List<ML.Usuario>) GetAll()
		{
			ML.Usuario usuario = new ML.Usuario();
			try
			{
				using (SqlConnection context = new SqlConnection(DL.Connection.GetConnectionString()))
				{
					string query = "UsuarioGetAll";

					SqlCommand command = new SqlCommand();
					command.Connection = context;
					command.CommandText = query;
					command.CommandType = System.Data.CommandType.StoredProcedure;

					SqlDataAdapter adapter = new SqlDataAdapter(command);

					DataTable table = new DataTable();

					adapter.Fill(table);

					if (table != null)
					{
						usuario.Usuarios = new List<ML.Usuario>();

						foreach (DataRow row in table.Rows)
						{
							ML.Usuario usuarioObj = new ML.Usuario();
							usuarioObj.IdUsuario = Convert.ToInt32(row[0].ToString());
							usuarioObj.Nombre = row[1].ToString();
							usuarioObj.ApellidoPaterno = row[2].ToString();
							usuarioObj.ApellidoMaterno = row[3].ToString();
							usuarioObj.Edad = Convert.ToInt32(row[4].ToString());
							usuarioObj.Email = row[5].ToString();

							usuario.Usuarios.Add(usuarioObj);
						}
						return (true, null, usuario.Usuarios);
					}
					else
					{
						return (false, "Error al obtener los registros", null);
					}
				}
			}
			catch (Exception ex)
			{
				return (false, ex.Message, null);
			}
		}

		public static (bool,string,ML.Usuario) GetById(int IdUsuario)
		{
			ML.Usuario usuario = new ML.Usuario();
			try
			{
				using (SqlConnection context = new SqlConnection(DL.Connection.GetConnectionString()))
				{
					string query = "UsuarioGetById";

					SqlCommand command = new SqlCommand();
					command.CommandText = query;
					command.Connection = context;
					command.CommandType = CommandType.StoredProcedure;

					SqlParameter[] parameter = new SqlParameter[1];

					parameter[0] = new SqlParameter("@IdUsuario", System.Data.SqlDbType.Int);
					parameter[0].Value = IdUsuario;

					command.Parameters.AddRange(parameter);

					SqlDataAdapter adapter = new SqlDataAdapter(command);
					DataTable table = new DataTable();

					adapter.Fill(table);

					if (table != null)
					{
						DataRow row = table.Rows[0];

						usuario.IdUsuario = Convert.ToInt32(row[0].ToString());
						usuario.Nombre = row[1].ToString();
						usuario.ApellidoPaterno = row[2].ToString();
						usuario.ApellidoMaterno = row[3].ToString();
						usuario.Edad = Convert.ToInt32(row[4].ToString());
						usuario.Email = row[5].ToString();

						return(true, null, usuario);
					}
					else
					{
						return(false, "Error al recuperar los registros", null);
					}
				}
			}
			catch (Exception ex)
			{
				return (false, ex.Message, null);
			}
		}
    }
}
