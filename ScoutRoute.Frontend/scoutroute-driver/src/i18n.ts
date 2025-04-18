import { notFound } from "next/navigation";
import { getRequestConfig, GetRequestConfigParams, RequestConfig } from "next-intl/server";

const locales = ["da"];

export default getRequestConfig(async ({ locale }: GetRequestConfigParams) => {
  console.log("getConfig");
  if (!locales.includes(locale ?? 'da')) return notFound();

  return {
    messages: (await import(`../messages/${locale}.json`)).default,
  } as RequestConfig;
});
