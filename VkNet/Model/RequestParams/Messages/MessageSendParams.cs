﻿#if WINDOWS_PHONE
	using System.Net;
#else
using System.Web;
#endif

using System.Collections.Generic;
using System.Net.Mail;

namespace VkNet.Model.RequestParams
{
    using Utils;

	/// <summary>
	/// Параметры метода messages.send
	/// </summary>
	public struct MessageSendParams
	{
		/// <summary>
		/// Идентификатор пользователя, которому отправляется сообщение.
		/// </summary>
		public long? UserId
		{ get; set; }

		/// <summary>
		/// Короткий адрес пользователя (например, illarionov).
		/// </summary>
		public string Domain
		{ get; set; }

		/// <summary>
		/// Идентификатор беседы, к которой будет относиться сообщение.
		/// </summary>
		public long? ChatId
		{ get; set; }

		/// <summary>
		/// Идентификаторы получателей сообщения (при необходимости отправить сообщение сразу нескольким пользователям).
		/// </summary>
		public IEnumerable<long> UserIds
		{ get; set; }

		/// <summary>
		/// Текст личного cообщения (является обязательным, если не задан параметр attachment)
		/// </summary>
		public string Message
		{ get; set; }

		/// <summary>
		/// Уникальный идентификатор, предназначенный для предотвращения повторной отправки одинакового сообщения.
		/// </summary>
		public long? Guid
		{ get; set; }

		/// <summary>
		/// Latitude - широта при добавлении местоположения.
		/// </summary>
		public double? Lat
		{ get; set; }

		/// <summary>
		/// Longitude - долгота при добавлении местоположения.
		/// </summary>
		public double? Longitude
		{ get; set; }

		/// <summary>
		/// Медиавложения к личному сообщению, перечисленные через запятую.
		/// </summary>
		public Attachment Attachment
		{ get; set; }

		/// <summary>
		/// Идентификаторы пересылаемых сообщений, перечисленные через запятую. Перечисленные сообщения отправителя будут отображаться в теле письма у получателя.
		/// </summary>
		public IEnumerable<ulong> ForwardMessages
		{ get; set; }

		/// <summary>
		/// Идентификатор стикера.
		/// </summary>
		public uint? StickerId
		{ get; set; }

		/// <summary>
		/// Привести к типу VkParameters.
		/// </summary>
		/// <param name="p">Параметры.</param>
		/// <returns></returns>
		internal static VkParameters ToVkParameters(MessageSendParams p)
        {
            return new VkParameters
            {
                { "user_id", p.UserId },
                { "domain", p.Domain },
                { "chat_id", p.ChatId },
                { "user_ids", p.UserIds },
                { "message", HttpUtility.UrlEncode(p.Message) },
                { "guid", p.Guid },
                { "lat", p.Lat },
                { "long", p.Longitude },
                { "attachment", p.Attachment },
                { "forward_messages", p.ForwardMessages },
                { "sticker_id", p.StickerId }
            };
        }
    }
}
