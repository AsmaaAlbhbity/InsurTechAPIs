import { HttpInterceptorFn } from '@angular/common/http';

export const tokenInterceptor: HttpInterceptorFn = (req, next) => {
  const token = (JSON.parse(localStorage.getItem('userData')??"{}"))["token"];
  if (token != "{}") {
    const clonedreq = req.clone({
      headers: req.headers.set('Authorization', `Bearer ${token}`)
    });
    return next(clonedreq);
  }
  else{
    return next(req);
  }
};
