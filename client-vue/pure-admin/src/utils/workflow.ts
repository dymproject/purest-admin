//类型过滤
export function cleanObject<T>(obj: T, keys: (keyof T)[]): Partial<T> {
  const result = {} as Partial<T>;
  for (const key of keys) {
    if (obj.hasOwnProperty(key)) {
      result[key] = obj[key];
    }
  }
  return result;
}
