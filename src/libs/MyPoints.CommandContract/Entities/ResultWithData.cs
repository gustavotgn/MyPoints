using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPoints.CommandContract.Entities
{
    public class ResultWithData<TData>
    {
        public bool Succeeded { get; protected set; }
        public TData Data { get; protected set; }

        public List<Notification> Errors { get; private set; } = null;

        public static ResultWithData<TData> Success(TData data)
        {
            return new ResultWithData<TData> {
                Succeeded = true,
                Data = data
            };
        }

        public static ResultWithData<TData> Failed(params Notification[] error)
        {
            var result = new ResultWithData<TData> { Succeeded = false, Data = default, Errors = error.ToList() };

            return result;
        }
        public static ResultWithData<TData> Failed(IReadOnlyCollection<Notification> error)
        {
            var result = new ResultWithData<TData> { Succeeded = false, Data = default, Errors = error.ToList() };

            return result;
        }

    }
}
