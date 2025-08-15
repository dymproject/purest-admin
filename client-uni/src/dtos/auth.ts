// 登陆模型
export type LoginInput = {
    account: string;
    password: string;
}

export type LoginOutput = {
    id: number
    name: string
    //avatar: string
}

export type UserInfoOutput = {
    id: number;
    name: string;
    email: string;
    phone: string;
    remark: string
}