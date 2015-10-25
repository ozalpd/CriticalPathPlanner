using CriticalPath.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CriticalPath.Web.Controllers
{
    public abstract partial class BaseController
    {
        protected virtual async Task<Company> FindAsyncCompany(int id)
        {
            return await DataContext.Companies.FirstOrDefaultAsync(x => x.Id == id);
        }

        protected virtual void Approve(IApproval approval)
        {
            approval.IsApproved = true;
            approval.ApproveDate = DateTime.Today;
            approval.ApprovedUserId = UserID;
            approval.ApprovedUserIp = GetUserIP();
        }

        protected virtual async Task ApproveSaveAsync(IApproval approval)
        {
            Approve(approval);
            await DataContext.SaveChangesAsync(this);
        }


        protected virtual async Task<bool> CanUserAddPurchaseOrder()
        {
            if (!_canUserAddPurchaseOrder.HasValue)
            {
                _canUserAddPurchaseOrder = Request.IsAuthenticated && (
                                    await IsUserAdminAsync() ||
                                    await IsUserSupervisorAsync() ||
                                    await IsUserClerkAsync());
            }
            return _canUserAddPurchaseOrder.Value;
        }
        bool? _canUserAddPurchaseOrder;

        protected virtual async Task<bool> CanUserAddContact()
        {
            if (!_canUserAddContact.HasValue)
            {
                _canUserAddContact = Request.IsAuthenticated && (
                                    await IsUserAdminAsync() ||
                                    await IsUserSupervisorAsync() ||
                                    await IsUserClerkAsync());
            }
            return _canUserAddContact.Value;
        }
        bool? _canUserAddContact;

        protected virtual async Task<bool> CanUserApprove()
        {
            if (!_canUserApprove.HasValue)
            {
                _canUserApprove = Request.IsAuthenticated && (
                                    await IsUserAdminAsync() ||
                                    await IsUserSupervisorAsync());
            }
            return _canUserApprove.Value;
        }
        bool? _canUserApprove;
    }
}