using EY.CTP.SRED.Platform.Service.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using TRIP.Platform.Service.Core.Entities;

namespace TRIP.Platform.Service.Infrastructure.Extensions
{
	public static class TypeExtensions
	{

		public static SqlDbType ToSqlDbType(this StoredProcParam parameter)
		{
			if (SqlDbTypeMap.ContainsKey(parameter.DATA_TYPE))
			{
				return SqlDbTypeMap[parameter.DATA_TYPE];
			}
			throw new ArgumentException($"{parameter.DATA_TYPE} is not a supported");
		}

		public static ParameterDirection ToParameterDirection(this StoredProcParam parameter)
		{
			if (ParameterDirectionMap.ContainsKey(parameter.PARAMETER_MODE))
			{
				return ParameterDirectionMap[parameter.PARAMETER_MODE];
			}
			else
			{
				return ParameterDirection.Input;
			}
		}

		public static Type ToType(this CustomTableTypeColumn type)
		{
			if (CustomTableTypeMap.ContainsKey(type.TYPE_NAME))
			{
				return CustomTableTypeMap[type.TYPE_NAME];
			}

			throw new ArgumentException($"{type.TYPE_NAME} is not a supported custom table type column");
		}

		public static object ToDefaultValue(this PropertyInfo propInfo, object obj)
		{
			var val = propInfo.GetValue(obj);
			if (val != null)
			{
				if (PropertyInfoDefaulValueMap.ContainsKey(val.GetType()))
				{
					return PropertyInfoDefaulValueMap[val.GetType()];
				}
			}
			return DBNull.Value;
		}

		private static readonly IDictionary<Type, object> PropertyInfoDefaulValueMap = new Dictionary<Type, object>()
		{
			{typeof(bool), false}
		};

		private static readonly IDictionary<string, SqlDbType> SqlDbTypeMap = new Dictionary<string, SqlDbType>(StringComparer.InvariantCultureIgnoreCase)
		{
			{"TinyInt",     SqlDbType.TinyInt   },
			{"SmallInt",    SqlDbType.SmallInt  },
			{"Int",     SqlDbType.Int   },
			{"BigInt",  SqlDbType.BigInt    },
			{"Image",   SqlDbType.Image },
			{"Bit",     SqlDbType.Bit   },
			{"Date",    SqlDbType.DateTime},
			{"DateTime",    SqlDbType.DateTime  },
			{"DateTime2",   SqlDbType.DateTime2 },
			{"DateTimeOffset",  SqlDbType.DateTimeOffset    },
			{"NVarChar",    SqlDbType.NVarChar  },
			{"VarChar",     SqlDbType.VarChar   },
			{"Text",    SqlDbType.Text  },
			{"NText",   SqlDbType.NText },
			{"Char",    SqlDbType.Char  },
			{"NChar",   SqlDbType.NChar },
			{"Money",   SqlDbType.Money },
			{"Real",    SqlDbType.Real  },
			{"Float",   SqlDbType.Float },
			{"Time",    SqlDbType.Time  },
			{"Numeric", SqlDbType.Money },
			{"Uniqueidentifier", SqlDbType.UniqueIdentifier }
		};

		private static readonly IDictionary<string, ParameterDirection> ParameterDirectionMap = new Dictionary<string, ParameterDirection>(StringComparer.InvariantCultureIgnoreCase)
		{
			{"out", ParameterDirection.Output },
			{"inout", ParameterDirection.InputOutput },
			{"in", ParameterDirection.Input}
		};

		private static readonly IDictionary<string, Type> CustomTableTypeMap = new Dictionary<string, Type>(StringComparer.InvariantCultureIgnoreCase)
		{
			{"TinyInt", typeof(byte) },
			{"SmallInt", typeof(short) },
			{"Int", typeof(int)},
			{"BigInt", typeof(long) },
			{"Image", typeof(byte[])},
			{"Bit", typeof(bool)},
			{"Date", typeof(DateTime)},
			{"DateTime", typeof(DateTime)},
			{"DateTime2", typeof(DateTime)},
			{"DateTimeOffset", typeof(DateTimeOffset)},
			{"NVarChar", typeof(string) },
			{"VarChar", typeof(string)},
			{"Text", typeof(string)},
			{"NText", typeof(string)},
			{"Char", typeof(char)},
			{"NChar", typeof(char)},
			{"Money", typeof(decimal)},
			{"Real", typeof(float)},
			{"Float", typeof(double)},
			{"Time", typeof(TimeSpan)},
			{"Numeric", typeof(decimal)},
			{"Decimal", typeof(decimal)},
			{"Uniqueidentifier", typeof(Guid)}
		};

	}
}
