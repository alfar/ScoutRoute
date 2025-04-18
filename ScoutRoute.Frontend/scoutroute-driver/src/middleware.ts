import createMiddleware from 'next-intl/middleware';

export default createMiddleware({
    locales: ['da'],
    defaultLocale: 'da'
});

export const config = {
    matcher: ['/', '/(da|en)/:path*']
};
