﻿namespace VkNet.Categories
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using JetBrains.Annotations;

	using Utils;

	using Model.Attachments;
	using Model;

	/// <summary>
	/// Методы для работы с документами (получение списка, загрузка, удаление и т.д.)
	/// </summary>
	public class DocsCategory
	{
		/// <summary>
		/// API
		/// </summary>
		private readonly VkApi _vk;

		/// <summary>
		/// Методы для работы с документами (получение списка, загрузка, удаление и т.д.).
		/// </summary>
		/// <param name="vk">API.</param>
		internal DocsCategory(VkApi vk)
		{
			_vk = vk;
		}

		/// <summary>
		/// Возвращает расширенную информацию о документах пользователя или сообщества.
		/// </summary>
		/// <param name="totalCount">Общее количество документов.</param>
		/// <param name="count">Количество документов, информацию о которых нужно вернуть. По умолчанию — все документы.</param>
		/// <param name="offset">Смещение, необходимое для выборки определенного подмножества документов. Положительное число.</param>
		/// <param name="ownerId">Идентификатор пользователя или сообщества, которому принадлежат документы. Целое число, по умолчанию идентификатор текущего пользователя.</param>
		/// <returns>
		/// После успешного выполнения возвращает список объектов документов.
		/// </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <see href="http://vk.com/dev/docs.get" />.
		/// </remarks>
		[Pure]
		[ApiVersion("5.40")]
		public ReadOnlyCollection<Document> Get(out int totalCount, int? count = null, int? offset = null, long? ownerId = null)
		{
			VkErrors.ThrowIfNumberIsNegative(() => count);
			VkErrors.ThrowIfNumberIsNegative(() => offset);

			var parameters = new VkParameters{ { "count", count }, { "offset", offset }, { "owner_id", ownerId } };

			var response = _vk.Call("docs.get", parameters);

			totalCount = response["count"];

			VkResponseArray items = response["items"];
			return items.ToReadOnlyCollectionOf<Document>(r => r);
		}

		/// <summary>
		/// Возвращает расширенную информацию о документах пользователя или сообщества.
		/// </summary>
		/// <param name="count">Количество документов, информацию о которых нужно вернуть. По умолчанию — все документы.</param>
		/// <param name="offset">Смещение, необходимое для выборки определенного подмножества документов. Положительное число.</param>
		/// <param name="ownerId">Идентификатор пользователя или сообщества, которому принадлежат документы. Целое число, по умолчанию идентификатор текущего пользователя.</param>
		/// <returns>
		/// После успешного выполнения возвращает список объектов документов.
		/// </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <see href="http://vk.com/dev/docs.get" />.
		/// </remarks>
		[Pure]
		[ApiVersion("5.40")]
		public ReadOnlyCollection<Document> Get(int? count = null, int? offset = null, long? ownerId = null)
		{
			int totalCount;
			return Get(out totalCount, count, offset, ownerId);
		}

		/// <summary>
		/// Возвращает информацию о документах по их идентификаторам.
		/// </summary>
		/// <param name="docs">Идентификаторы документов, информацию о которых нужно вернуть.</param>
		/// <returns>После успешного выполнения возвращает список объектов документов.</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <see href="http://vk.com/dev/docs.getById"/>.
		/// </remarks>
		[Pure]
		[ApiVersion("5.40")]
		public ReadOnlyCollection<Document> GetById(IEnumerable<Document> docs)
		{
			foreach (var doc in docs)
			{
				VkErrors.ThrowIfNumberIsNegative(() => doc.Id);
				VkErrors.ThrowIfNumberIsNegative(() => doc.OwnerId);
			}

			var parameters = new VkParameters
			{
				{ "docs", string.Concat(docs.Select(it => it.OwnerId + "_" + it.Id + ",")) }
			};

			var response = _vk.Call("docs.getById", parameters);
			return response.ToReadOnlyCollectionOf<Document>(r => r);
		}

		/// <summary>
		/// Возвращает адрес сервера для загрузки документов. 
		/// </summary>
		/// <param name="groupId">Идентификатор сообщества (если необходимо загрузить документ в список документов сообщества). Если документ нужно загрузить в список пользователя, метод вызывается без дополнительных параметров. Положительное число</param>
		/// <returns>После успешного выполнения возвращает объект <see cref="UploadServerInfo"/></returns>
		/// <remarks>
		/// Страница документации ВКонтакте <see href="http://vk.com/dev/docs.getUploadServer"/>.
		/// </remarks>
		[Pure]
		[ApiVersion("5.40")]
		public UploadServerInfo GetUploadServer(long? groupId = null)
		{
			VkErrors.ThrowIfNumberIsNegative(() => groupId);

			var parameters = new VkParameters
			{
				{ "group_id", groupId }
			};

			return _vk.Call("docs.getUploadServer", parameters);
		}

		/// <summary>
		/// Возвращает адрес сервера для загрузки документов в папку Отправленные, для последующей отправки документа на стену или личным сообщением. 
		/// </summary>
		/// <param name="groupId">Идентификатор сообщества, в которое нужно загрузить документ. Положительное число.</param>
		/// <returns>После успешного выполнения возвращает объект <see cref="UploadServerInfo"/></returns>
		/// <remarks>
		/// Страница документации ВКонтакте <see href="http://vk.com/dev/docs.getWallUploadServer"/>.
		/// </remarks>
		[Pure]
		[ApiVersion("5.40")]
		public UploadServerInfo GetWallUploadServer(long? groupId = null)
		{
			VkErrors.ThrowIfNumberIsNegative(() => groupId);

			var parameters = new VkParameters { { "group_id", groupId } };

			return _vk.Call("docs.getWallUploadServer", parameters);
		}

		/// <summary>
		/// Сохраняет документ после его успешной загрузки на сервер.
		/// </summary>
		/// <param name="file">Параметр, возвращаемый в результате загрузки файла на сервер. Обязательный параметр.</param>
		/// <param name="title">Название документа.</param>
		/// <param name="tags">Метки для поиска.</param>
		/// <param name="captchaSid">Id капчи (только если для вызова метода необходимо ввести капчу)</param>
		/// <param name="captchaKey">Текст капчи (только если для вызова метода необходимо ввести капчу)</param>
		/// <returns>Возвращает массив с загруженными объектами. </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <see href="http://vk.com/dev/docs.save"/>.
		/// </remarks>
		[Pure]
		[ApiVersion("5.40")]
		public ReadOnlyCollection<Document> Save(string file, string title, string tags = null, long? captchaSid = null, string captchaKey = null)
		{
			VkErrors.ThrowIfNullOrEmpty(() => file);
			VkErrors.ThrowIfNullOrEmpty(() => title);

			var parameters = new VkParameters
			{
				{ "file", file },
				{ "title", title },
				{ "tags", tags },
				{ "captcha_sid", captchaSid },
				{ "captcha_key", captchaKey }
			};

			var response = _vk.Call("docs.save", parameters);
			return response.ToReadOnlyCollectionOf<Document>(r => r);
		}

		/// <summary>
		/// Удаляет документ пользователя или группы. 
		/// </summary>
		/// <param name="ownerId">Идентификатор пользователя или сообщества, которому принадлежит документ. Целое число, обязательный параметр</param>
		/// <param name="docId">Идентификатор документа. Положительное число, обязательный параметр</param>
		/// <returns>После успешного выполнения возвращает 1. </returns>
		/// <remarks>
		/// Страница документации ВКонтакте <see href="http://vk.com/dev/docs.delete"/>.
		/// </remarks>
		[Pure]
		[ApiVersion("5.40")]
		public int Delete(long ownerId, long docId)
		{
			VkErrors.ThrowIfNumberIsNegative(() => ownerId);
			VkErrors.ThrowIfNumberIsNegative(() => docId);

			var parameters = new VkParameters
			{
				{ "owner_id", ownerId },
				{ "doc_id", docId }
			};
			return _vk.Call("docs.delete", parameters);
		}

		/// <summary>
		/// Копирует документ в документы текущего пользователя.
		/// </summary>
		/// <param name="ownerId">Идентификатор пользователя или сообщества, которому принадлежит документ. Целое число, обязательный параметр</param>
		/// <param name="docId">Идентификатор документа. Положительное число, обязательный параметр</param>
		/// <param name="accessKey">Ключ доступа документа. Этот параметр следует передать, если вместе с остальными данными о документе было возвращено поле access_key.</param>
		/// <returns>После успешного выполнения возвращает идентификатор созданного документа (did).</returns>
		/// <remarks>
		/// Страница документации ВКонтакте <see href="http://vk.com/dev/docs.add"/>.
		/// </remarks>
		[Pure]
		[ApiVersion("5.40")]
		public int Add(long ownerId, long docId, string accessKey = null)
		{
			VkErrors.ThrowIfNumberIsNegative(() => ownerId);
			VkErrors.ThrowIfNumberIsNegative(() => docId);

			var parameters = new VkParameters { { "owner_id", ownerId }, { "doc_id", docId }, { "access_key", accessKey } };

			return _vk.Call("docs.add", parameters);
		}
	}
}