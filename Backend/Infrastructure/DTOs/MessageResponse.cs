using Infrastructure.Entities.DataTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTOs
{
    public class MessageResponseWithObj<T>
    {
        public string Message { get; private set; }
        public T Data { get; private set; }


        internal MessageResponseWithObj(string message, T data)
        {
            this.Data = data;
            this.Message = message;
        }
    }


    public class MessageResponseWithDataTable<T>
    {
        public string Message { get; private set; }
        public T Data { get; private set; }
        public int Count { get; private set; }


        internal MessageResponseWithDataTable(string message, T data, int count, int skip, int take)
        {
            Data = data;
            Message = message;
            Count = count;
        }
    }


    public class MessageResponse
    {
        public string Message { get; private set; }


        internal MessageResponse(string message)
        {
            Message = message;
        }
    }


    public class MessageResponseFactory
    {
        public static MessageResponse Create(string message)
        {
            return new MessageResponse(message);
        }

        public static MessageResponseWithObj<T> Create<T>(string message, T data)
        {
            return new MessageResponseWithObj<T>(message, data);
        }

        public static MessageResponseWithDataTable<IEnumerable<T>> Create<T>(string message, (int count, IEnumerable<T> list) data, DataTableDTOs.Paginate paginate)
        {
            return new MessageResponseWithDataTable<IEnumerable<T>>(message, data.list, data.count, paginate.skip, paginate.take);
        }
    }
}
