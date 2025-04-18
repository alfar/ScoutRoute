import { AbstractIntlMessages, NextIntlClientProvider } from "next-intl";
import { getMessages } from "next-intl/server";

export default async function LocaleLayout({
  children,
  params: { locale },
}: {
  children: React.ReactNode;
  params: { locale: string };
}) {
  const messages = await getMessages();

  return (
    <html lang={locale}>
      <body>
        <NextIntlClientProvider
          messages={JSON.stringify(messages) as unknown as AbstractIntlMessages}
        >
          {children}
        </NextIntlClientProvider>
      </body>
    </html>
  );
}
