export interface INavigationMenu {
    title: string;
    links: ILink[];
    permissions: string[];
}

interface ILink {
    icon: string;
    title: string;
    link: string;
    permissions: string[];
}