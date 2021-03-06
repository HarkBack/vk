﻿using VkNet.Model.RequestParams;

namespace VkNet.Categories
{
    using System.Collections.ObjectModel;

    using Enums.SafetyEnums;
    using Utils;
    using Model;

	/// <summary>
	/// API для работы с лайками.
	/// </summary>
	public class LikesCategory
    {
		/// <summary>
		/// API
		/// </summary>
		private readonly VkApi _vk;

		/// <summary>
		/// API для работы с лайками.
		/// </summary>
		/// <param name="vk">The vk.</param>
		internal LikesCategory(VkApi vk)
        {
            _vk = vk;
        }

		/// <summary>
		/// Получает список идентификаторов пользователей или сообществ, которые добавили заданный объект в свой список Мне нравится.
		/// </summary>
		/// <param name="params">Параметры запроса.</param>
		/// <returns>
		/// Возвращает список идентификаторов пользователей или сообществ, которые добавили заданный объект в свой список Мне нравится.
		/// </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <see href="http://vk.com/dev/likes.getList"/>.
		/// </remarks>
		[ApiVersion("5.37")]
		public ReadOnlyCollection<long> GetList(LikesGetListParams @params)
		{


			VkResponseArray response = _vk.Call("likes.getList", @params);

			return response.ToReadOnlyCollectionOf<long>(x => x);
		}

		/// <summary>
		/// Получает список идентификаторов пользователей или сообществ, которые добавили заданный объект в свой список Мне нравится.
		/// </summary>
		/// <param name="params">Параметры запроса.</param>
		/// <returns>
		/// Возвращает список пользователей и сообществ, которые добавили заданный объект в свой список Мне нравится.
		/// </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <see href="http://vk.com/dev/likes.getList"/>.
		/// </remarks>
		[ApiVersion("5.37")]
		public UserOrGroup GetListEx(LikesGetListParams @params)
		{
			return _vk.Call("likes.getList", @params, true);
		}
		/// <summary>
		/// Добавляет указанный объект в список Мне нравится текущего пользователя.
		/// </summary>
		/// <param name="type">Тип объекта <see cref="LikeObjectType" /></param>
		/// <param name="itemId">Идентификатор объекта. положительное число, обязательный параметр</param>
		/// <param name="ownerId">Идентификатор владельца объекта. целое число, по умолчанию идентификатор текущего пользователя</param>
		/// <param name="accessKey">Ключ доступа в случае работы с приватными объектами. строка</param>
		/// <param name="reference">Ссылка.</param>
		/// <returns>
		/// В случае успеха возвращает объект с полем likes, в котором находится текущее количество пользователей, которые добавили данный объект в свой список Мне нравится.
		/// </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <see href="http://vk.com/dev/likes.add" />.
		/// </remarks>
		[ApiVersion("5.37")]
		public long Add(LikeObjectType type, ulong itemId, long? ownerId = null, string accessKey = null, string reference = null)
        {
            var parameters = new VkParameters
                {
                    { "type", type },
                    { "item_id", itemId },
                    { "owner_id", ownerId },
                    { "access_key", accessKey },
                    { "ref", reference }
                };

            var response = _vk.Call("likes.add", parameters);

            return response["likes"];
        }

		/// <summary>
		/// Удаляет указанный объект из списка Мне нравится текущего пользователя
		/// </summary>
		/// <param name="type">Тип объекта <see cref="LikeObjectType"/></param>
		/// <param name="itemId">Идентификатор объекта. положительное число, обязательный параметр</param>
		/// <param name="ownerId">Идентификатор владельца объекта. целое число, по умолчанию идентификатор текущего пользователя</param>
		/// <returns>В случае успеха возвращает объект с полем likes, в котором находится текущее количество пользователей, которые добавили данный объект в свой список Мне нравится. </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <see href="http://vk.com/dev/likes.delete"/>.
		/// </remarks>
		[ApiVersion("5.37")]
		public ulong Delete(LikeObjectType type, long itemId, ulong? ownerId = null)
        {
            var parameters = new VkParameters
                {
                    { "type", type },
                    { "item_id", itemId },
                    { "owner_id", ownerId }
                };

            var response = _vk.Call("likes.delete", parameters);

            return response["likes"];
        }
		/// <summary>
		/// Проверяет, находится ли объект в списке Мне нравится заданного пользователя.
		/// </summary>
		/// <param name="copied">Сделан ли репост текущим пользователем.</param>
		/// <param name="type">Тип объекта <see cref="LikeObjectType" /></param>
		/// <param name="itemId">Идентификатор объекта. положительное число, обязательный параметр</param>
		/// <param name="userId">Идентификатор пользователя, у которого необходимо проверить наличие объекта в списке «Мне нравится». Если параметр не задан, то считается, что он равен идентификатору текущего пользователя. положительное число, по умолчанию идентификатор текущего пользователя</param>
		/// <param name="ownerId">Идентификатор владельца Like-объекта. Если параметр не задан, то считается, что он равен идентификатору текущего пользователя. целое число, по умолчанию идентификатор текущего пользователя</param>
		/// <returns>
		/// В случае успеха возвращает одно из следующих числовых значений:
		/// false — указанный Like-объект не входит в список Мне нравится пользователя с идентификатором user_id.
		/// true — указанный Like-объект находится в списке Мне нравится пользователя с идентификатором user_id.
		/// </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <see href="http://vk.com/dev/likes.isLiked" />.
		/// </remarks>
		[ApiVersion("5.37")]
        public bool IsLiked(out bool copied, LikeObjectType type, ulong itemId,  ulong? userId = null, long? ownerId = null)
        {
            var parameters = new VkParameters
                {
                    { "type", type },
                    { "item_id", itemId },
                    { "user_id", userId },
                    { "owner_id", ownerId }
                };

            var resp = _vk.Call("likes.isLiked", parameters);

            copied = resp["copied"];
            return resp["liked"];
        }
    }
}
