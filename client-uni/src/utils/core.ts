/**
 * 获取完整请求的url
 * @param url url
 * @returns 完整url
 */
export const getRequestUrl = (url: string): string => {
    // 判断是否是完整的url
    if (!/^(http|https):\/\//.test(url)) {
        // 获取当前环境：development / production
        //const isDev = import.meta.env.DEV; // true 表示开发环境
        const baseUrl = import.meta.env.VITE_API_BASE_URL;
        const prefix = import.meta.env.VITE_API_PREFIX;

        const normalizedBaseUrl = baseUrl?.endsWith('/') ? baseUrl.slice(0, -1) : baseUrl;
        const normalizedPrefix = prefix?.startsWith('/') ? prefix : `/${prefix}`;
        const normalizedUrl = url.startsWith('/') ? url.slice(1) : url;

        return `${normalizedBaseUrl}${normalizedPrefix}/${normalizedUrl}`;
    }
    return url;
}
/**
 * 获取完整的signalr url
 * @param url url
 * @returns 完整signalr url
 */
export const getSignalRUrl = (url: string): string => {
    // 判断是否是完整的url
    if (!/^(http|https):\/\//.test(url)) {
        // 获取当前环境：development / production
        //const isDev = import.meta.env.DEV; // true 表示开发环境
        const baseUrl = import.meta.env.VITE_API_BASE_URL;
        const prefix = import.meta.env.VITE_SIGNALR_PREFIX;

        const normalizedBaseUrl = baseUrl?.endsWith('/') ? baseUrl.slice(0, -1) : baseUrl;
        const normalizedPrefix = prefix?.startsWith('/') ? prefix : `/${prefix}`;
        const normalizedUrl = url.startsWith('/') ? url.slice(1) : url;

        return `${normalizedBaseUrl}${normalizedPrefix}/${normalizedUrl}`;
    }
    return url;
}