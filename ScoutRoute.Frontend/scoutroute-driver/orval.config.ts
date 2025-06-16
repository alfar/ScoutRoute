import { defineConfig } from "orval";

export default defineConfig({
    routes: {
        input: "./src/api/openapi.json",        
        output: {
            mode: "split",
            target: "./src/api/scoutroute.ts",
            baseUrl: "https://localhost:7520/",
            schemas: "./src/models",
            client: "react-query",
        },
        hooks: {
            afterAllFilesWrite: "prettier --write",
        }
    },
});