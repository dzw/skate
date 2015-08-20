//
//  SPCredentials.h
//  SponsorPay iOS SDK
//
//  Copyright (c) 2012 SponsorPay. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface SPCredentials : NSObject<NSCopying>

@property (nonatomic, copy) NSString *appId;
@property (nonatomic, copy) NSString *userId;
@property (nonatomic, copy) NSString *securityToken;
@property (nonatomic, weak, readonly) NSString *credentialsToken;
@property (nonatomic, readonly) NSMutableDictionary *userConfig;

+ (SPCredentials *)credentialsWithAppId:(NSString *)appId userId:(NSString *)userId securityToken:(NSString *)securityToken;

+ (NSString *)credentialsTokenForAppId:(NSString *)appId userId:(NSString *)userId;

FOUNDATION_EXPORT NSString *const SPVCSConfigCurrencyName;

@end
