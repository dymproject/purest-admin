import { http } from "@/utils/http";
import { PureHttpResponse } from "@/utils/http/types";
import { ElLoading } from "element-plus";

export const exportFile = (url: string, params: any) => {
  const loading = ElLoading.service({
    lock: true,
    text: "正在导出……",
    background: "rgba(0, 0, 0, 0.7)"
  });
  http
    .request("get", url, {
      responseType: "blob",
      params
    })
    .then((response: PureHttpResponse) => {
      const { headers } = response;
      const {
        "content-type": contentType,
        "content-disposition": contentDisposition
      } = headers;
      const blob = new Blob([response.data], {
        type: contentType
      });
      const pattern = new RegExp("filename*=([^;]+\\.[^\\.;]+);*");
      const result = pattern.exec(contentDisposition) as RegExpExecArray;
      const filename = decodeURI(result[1]); // 处理文件名,解决中文乱码问题
      const downloadElement = document.createElement("a");
      const href = window.URL.createObjectURL(blob); // 创建下载的链接
      downloadElement.style.display = "none";
      downloadElement.href = href;
      downloadElement.download = filename; // 下载后文件名
      document.body.appendChild(downloadElement);
      downloadElement.click(); // 点击下载
      document.body.removeChild(downloadElement); // 下载完成移除元素
      window.URL.revokeObjectURL(href);
    })
    .finally(() => {
      loading.close();
    });
};
