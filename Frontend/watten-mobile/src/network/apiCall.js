import HttpRequest from "./HttpRequest";
export default function apiCall(url, method, body, contentType) {
  const promise = new Promise((resolve, reject) => {
    HttpRequest(url, method, body, contentType)
      .then((result) => {
        resolve(result?.data);
      })
      .catch((e) => {
        reject(e);
      });
  });
  return promise;
}
