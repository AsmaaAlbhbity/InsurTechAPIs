import { CanActivateFn, Router } from '@angular/router';

export const authUserGuard: CanActivateFn = (route, state) => {
  const role = (JSON.parse(localStorage.getItem('userData')??"{}"))["userType"];
  if (role == "0") {
    return true;
  }
  else{
    const router = new Router();
    router.navigate(["/login/notAuthorized"]);
    return false;
  }
};