﻿using VkNet.Enums.SafetyEnums;
using VkNet.Utils;

namespace VkNet.Model
{
	/// <summary>
	/// Информация о текущем роде занятия пользователя.
	/// </summary>
	public class Occupation
	{
		/// <summary>
		/// Название школы, вуза или места работы
		/// </summary>
		public string Name;

		/// <summary>
		/// Идентификатор школы, вуза, группы компании (в которой пользователь работает).
		/// </summary>
		public long Id;

		/// <summary>
		/// Информация о текущем роде занятия пользователя.
		/// </summary>
		public OccupationType Type;

		/// <summary>
		/// Разобрать из json.
		/// </summary>
		/// <param name="response">Ответ сервера.</param>
		/// <returns></returns>
		internal static Occupation FromJson(VkResponse response)
		{
			var occupation = new Occupation
			{
				Id = response["id"],
				Name = response["name"],
				Type = response["type"]
			};

			return occupation;
		}
	}
}