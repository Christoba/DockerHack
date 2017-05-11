import { inject } from "aurelia-framework";
import { HttpClient } from "aurelia-http-client";

let baseUrl = "/api/custodians";

@inject(HttpClient)
export class CustodianData {

    constructor(httpClient) {
        this.http = httpClient;
    }

    getAll() {
        return this.http.get(baseUrl)
            .then(response => {
                return response.content;
            });
    }
}