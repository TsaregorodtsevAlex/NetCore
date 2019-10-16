using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Scheduling;

namespace NetCoreDataBus
{
	public static class BusExtensions
	{
		public static async Task<Guid> SendScheduledMessage<TMessage>(this IBus bus, TMessage message, DateTime scheduledTime)
		{
			var sendResult = await bus.ScheduleSend(new Uri($"rabbitmq://{bus.Address.Host}/{typeof(TMessage).Namespace}:{typeof(TMessage).Name}"), scheduledTime, message);
			return sendResult.TokenId;
		}

		public static async Task CancelScheduledMessage(this IBus bus, Guid tokenId)
		{
			await bus.CancelScheduledSend(tokenId);
		}

		public static async Task<ScheduledRecurringMessage> ScheduleRecurringSend<TMessage, TRecurringSchedule>(this IBus bus, TRecurringSchedule recurringSchedule, TMessage message)
			where TRecurringSchedule : DefaultRecurringSchedule
		{
			var scheduledRecurringMessage = await bus.ScheduleRecurringSend(new Uri($"rabbitmq://{bus.Address.Host}/{typeof(TMessage).Namespace}:{typeof(TMessage).Name}"), recurringSchedule, message);

			return scheduledRecurringMessage;
		}
		public static async Task CancelScheduledRecurringMessage<TMessage>(this IBus bus, ScheduledRecurringMessage<TMessage> recurringMessage)
			where TMessage : class
		{
			await bus.CancelScheduledRecurringSend(recurringMessage);
		}
		public static async Task CancelScheduledRecurringMessage<TMessage>(this IBus bus, string scheduleId, string scheduleGroup)
			where TMessage : class
		{
			await bus.CancelScheduledRecurringSend(scheduleId, scheduleGroup);
		}
	}
}
