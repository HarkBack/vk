﻿using VkNet.Model.Attachments;
using VkNet.Utils;

namespace VkNet.Model
{
	/// <summary>
	/// Возвращает данные о точках, по которым вырезаны профильная и миниатюрная фотографии пользователя.
	/// </summary>
	public class CropPhoto
	{
		/// <summary>
		/// Объект photo фотографии пользователя из которой вырезается профильная аватарка.
		/// </summary>
		public Photo Photo;

		/// <summary>
		/// Вырезанная фотография пользователя, поля: x, y, x2, y2, координаты указаны в процентах.
		/// </summary>
		public Rect Crop;

		/// <summary>
		/// Миниатюрная квадратная фотография, вырезанная из фотографии Crop: x, y, x2, y2, координаты также указаны в процентах;
		/// </summary>
		public Rect Rect;

		
		/// <summary>
		/// Разобрать из json.
		/// </summary>
		/// <param name="response">Ответ сервера.</param>
		/// <returns></returns>
		internal static CropPhoto FromJson(VkResponse response)
		{
			var cropPhoto = new CropPhoto
			{
				Photo = response["photo"],
				Crop = response["crop"],
				Rect = response["rect"]
			};

			return cropPhoto;
		}
	}
}