import { CanActivateFn, Router } from '@angular/router';

export const authAdminGuard: CanActivateFn = (route, state) => {
  const role = (JSON.parse(localStorage.getItem('userData')??"{}"))["userType"];
  if (role == "2") {
    return true;
  }
  else{
    const router = new Router();
    router.navigate(["/login/notAuthorized"]);
    return false;
  }
};
