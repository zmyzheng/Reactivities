using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities
{
    public class Unattend
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                this._userAccessor = userAccessor;
                this._context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var actvity = await _context.Activities.FindAsync(request.Id);
                if (actvity == null)
                {
                    throw new RestException(HttpStatusCode.NotFound,
                        new { Message = "Could not find activity"});
                }

                var user = await _context.Users.SingleOrDefaultAsync(x => 
                    x.UserName == _userAccessor.GetCurrentUsername());
                
                var attendance = await _context.UserActivities.SingleOrDefaultAsync(x =>
                    x.ActivityId == actvity.Id && x.AppUserId == user.Id);

                if (attendance == null)
                {
                    return Unit.Value;
                }

                if (attendance.IsHost)
                {
                    throw new RestException(HttpStatusCode.BadRequest, 
                        new { Message = "You cannot remove yourself as host"});
                }

                _context.UserActivities.Remove(attendance);

                var success = await _context.SaveChangesAsync() > 0;
                if (success)
                {
                    return Unit.Value;
                }
                else
                {
                    throw new Exception("Problem saving changes");
                }
            }
        }
    }
}